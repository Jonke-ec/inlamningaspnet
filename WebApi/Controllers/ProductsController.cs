#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi;
using WebApi.Models;
using WebApi.Models.Entities;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [ApiKey]

    public class ProductsController : ControllerBase
    {
        private readonly SqlContext _context;

        public ProductsController(SqlContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductOutputModel>>> GetProducts()
        {
            var items = new List<ProductOutputModel>();
            foreach (var i in await _context.Products.Include(x => x.Category).ToListAsync())
                items.Add(new ProductOutputModel(i.Id, i.ArticleNumber, i.ProductName, i.Description, i.Price, new CategoryModel(i.Category.CategoryName)));
            return items;
            
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductOutputModel>> GetProduct(int id)
        {
            var productEntity = await _context.Products.Include(x => x.Category).FirstOrDefaultAsync(x => x.Id == id);

            if (productEntity == null)
            {
                return NotFound();
            }

            return new ProductOutputModel(productEntity.Id, productEntity.ArticleNumber, productEntity.ProductName, productEntity.Description, productEntity.Price, new CategoryModel(productEntity.Category.CategoryName));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, ProductUpdateModel productUpdateModel)
        {
            if (id != productUpdateModel.Id)
            {
                return BadRequest();
            }

            var productEntity = await _context.Products.FindAsync(productUpdateModel.Id);

            productEntity.ArticleNumber = productUpdateModel.ArticleNumber;
            productEntity.ProductName = productUpdateModel.ProductName;
            productEntity.Description = productUpdateModel.Description;
            productEntity.Price = productUpdateModel.Price;

            _context.Entry(productUpdateModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductEntityExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        [HttpPost]
        public async Task<ActionResult<ProductOutputModel>> PostProduct(ProductInputModel productModel)
        {
            if (await _context.Products.AnyAsync(x => x.ArticleNumber == productModel.ArticleNumber))
                return BadRequest();

            var productEntity = new ProductEntity(productModel.ArticleNumber, productModel.ProductName, productModel.Description, productModel.Price);

            var category = await _context.Categories.FirstOrDefaultAsync(x => x.CategoryName == productModel.CatogoryName);
            if (category != null)
                productEntity.CategoryId = category.Id;
            else
                productEntity.Category = new CategoryEntity(productModel.CatogoryName);

            _context.Products.Add(productEntity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = productEntity.Id },
                new ProductOutputModel(productEntity.Id, productEntity.ArticleNumber, productEntity.ProductName, productEntity.Description, productEntity.Price, 
                new CategoryModel(productEntity.Category.CategoryName)));
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var productEntity = await _context.Products.FindAsync(id);
            if (productEntity == null)
            {
                return NotFound();
            }

            _context.Products.Remove(productEntity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductEntityExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
