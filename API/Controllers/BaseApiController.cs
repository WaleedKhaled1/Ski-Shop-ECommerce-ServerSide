using API.Helpers;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController:ControllerBase
    {
        protected async Task<ActionResult> CreatePagination<T>(IGenericRepository<T> repo,ISpecification<T> spec,
            int PageNumber,int PageSize) 
            where T:BaseEntity
        {
            var items = await repo.ListWithSpecAsync(spec);

            var Count = await repo.GetCountAsync(spec);

            var Pagination = new Pagination<T>(PageNumber, PageSize, Count, items);

            return Ok(Pagination);
        }
    }
}
