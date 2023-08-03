using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Domain.Entities;
using ETicaretAPI.Persistence.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        readonly private IProductWriteRepository _productWriteRepository;
        readonly private IProductReadRepository _productReadRepository;

        public ProductsController(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository)
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
        }


        [HttpGet]

        public async Task Get()
        {
            //await _productWriteRepository.AddRangeAsync(new()
            //{
            //    new() { Id = Guid.NewGuid(), Name= "Product 1", CreatedDate=DateTime.UtcNow, Price = 100, Stock = 10 },
            //    new() { Id = Guid.NewGuid(), Name= "Product 2", CreatedDate=DateTime.UtcNow, Price = 200, Stock = 20 },
            //    new() { Id = Guid.NewGuid(), Name= "Product 3", CreatedDate=DateTime.UtcNow, Price = 300, Stock = 30 }
            //    });
            //var count = await _productWriteRepository.SaveAsync();

            Product p = await _productReadRepository.GetByIdAsymc("e1d2608f-2a94-43cf-84e2-5a5111b5a1b8", false); 
            //Deyishikliklere icaze vermir
            p.Name = "kanan";
            await _productWriteRepository.SaveAsync();

        }


        [HttpGet("{id}")]

        public async Task<IActionResult> Get(string id)
            {
                Product product = await _productReadRepository.GetByIdAsymc(id);
                return Ok(product);
            }



        }

    }
