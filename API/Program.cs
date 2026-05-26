using API.Middleware;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
    
    // Add services to the container.

    builder.Services.AddControllers();
builder.Services.AddDbContext<StoreDbContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<IProductRepository,ProductRepository>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

builder.Services.AddCors();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddlware>(); 
    app.UseHttpsRedirection();

app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins("http//localhost:4200",
    "https:localhost:4200"));

    app.UseAuthorization();

    app.MapControllers();

     try
     {
        using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<StoreDbContext>();
        await context.Database.MigrateAsync();
        await StoreProductsSeed.SeedAsync(context);
    }
}
     
     catch (Exception ex)
     {
         Console.WriteLine(ex.Message);
     }


    app.Run();
