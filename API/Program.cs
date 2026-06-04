using API.Middleware;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);
    
    // Add services to the container.

    builder.Services.AddControllers();
builder.Services.AddDbContext<StoreDbContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<IProductRepository,ProductRepository>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

builder.Services.AddSingleton<ICartService, CartService>();
builder.Services.AddSingleton<IConnectionMultiplexer>(config =>
{
    var connString = builder.Configuration.GetConnectionString("Redis")
    ?? throw new Exception("Connection string for Redis is not found.");

    var configuration = ConfigurationOptions.Parse(connString, true);

    return ConnectionMultiplexer.Connect(configuration);
});

builder.Services.AddCors();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddlware>(); 
    app.UseHttpsRedirection();

app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200",
    "https://localhost:4200"));

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
