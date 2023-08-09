﻿using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Domain.Entities;
using ETicaretAPI.Persistence.Repositories;
using Microsoft.AspNetCore.Mvc;
using ETicaretAPI.Application.ViewModels.Products;
using System.Net;
using ETicaretAPI.Application.RequestParametrs;

namespace ETicaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        readonly private IProductWriteRepository _productWriteRepository;
        readonly private IProductReadRepository _productReadRepository;


        public ProductsController(
            IProductWriteRepository productWriteRepository,
            IProductReadRepository productReadRepository)

        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;

        }


        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]Pagination pagination)
        {
            await Task.Delay(1000);
            var totalCount = _productReadRepository.GetAll(false).Count();
           var products =  _productReadRepository.GetAll(false).Skip(pagination.Page * pagination.Size).Take(pagination.Size).
                Select(p => new
            {
                p.Id,
                p.Name,
                p.Stock,
                p.Price,
                p.CreatedDate,
                p.UpdatedDate

            }).ToList();

            return Ok(new 
            { 
                totalCount,
                products
            
            });
        }



        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id) 
        {
            return Ok(await _productReadRepository.GetByIdAsymc(id, false));
        }





        [HttpPost]
        public async Task<IActionResult> Post(VM_Create_Product model)
        {


            await _productWriteRepository.AddAsync( new()
            { 
                Name = model.Name,
                Price = model.Price,
                Stock = model.Stock
            });

            await _productWriteRepository.SaveAsync();

            return StatusCode((int)HttpStatusCode.Created);
        }



        [HttpPut]

        public async Task<IActionResult> Put(VM_Update_Product model)
        {
            Product product =await _productReadRepository.GetByIdAsymc(model.Id);
            product.Name = model.Name;
            product.Price = model.Price;
            product.Stock = model.Stock;
            await _productWriteRepository.SaveAsync();
            return Ok();
        }


        [HttpDelete("{id}")]

        public async Task<IActionResult> Delete(string id) 
        {
            await _productWriteRepository.RemoveAsync(id);
            await _productWriteRepository.SaveAsync();
            return Ok();

        }

    }

    }
