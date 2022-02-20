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

    public class OrdersController : ControllerBase
    {
        private readonly SqlContext _context;

        public OrdersController(SqlContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderOutputModel>>> GetOrders()
        {
            var items = new List<OrderOutputModel>();
            foreach (var i in await _context.Orders.Include(x => x.User).Include(x => x.Product).ToListAsync())
                items.Add(new OrderOutputModel(i.Id, i.Created, i.Quantity, new UserEntity(i.User.Id, i.User.Email), new ProductEntity(i.Product.Id, i.Product.ArticleNumber, i.Product.ProductName, i.Product.Price)));
            return items;
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<OrderOutputModel>> GetOrder(int id)
        {
            var orderEntity = await _context.Orders.Include(x => x.User).Include(x => x.Product).FirstOrDefaultAsync(x => x.Id == id);

            if (orderEntity == null) {
                return NotFound();
            }

            return new OrderOutputModel(orderEntity.Id, orderEntity.Created, orderEntity.Quantity, new UserEntity(orderEntity.User.Id, orderEntity.User.Email), new ProductEntity(orderEntity.Product.Id, orderEntity.Product.ArticleNumber, orderEntity.Product.ProductName, orderEntity.Product.Price));
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, OrderUpdateModel model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }

            var orderEntity = await _context.Orders.FindAsync(model.Id);

            orderEntity.Quantity = model.Quantity;

            _context.Entry(orderEntity).State = EntityState.Modified;

            try {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) {
                if (!OrderEntityExists(id)) {
                    return NotFound();
                }
                else {
                    throw;
                }
            }

            return NoContent();
        }


        [HttpPost]
        public async Task<ActionResult<OrderOutputModel>> PostOrder(OrderInputModel model)
        {
            var orderEntity = new OrderEntity(model.Quantity);

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == model.UserId);
            if (user != null)
                orderEntity.UserId = user.Id;
            else
                orderEntity.User = new UserEntity(model.UserId);

            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == model.ProductId);
            if (product != null)
                orderEntity.ProductId = product.Id;
            else
                orderEntity.Product = new ProductEntity(model.ProductId);

            _context.Orders.Add(orderEntity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrder", new { id = orderEntity.Id },
                new OrderOutputModel(orderEntity.Id, orderEntity.Created, orderEntity.Quantity, new UserEntity(orderEntity.User.Id, orderEntity.User.Email), new ProductEntity(orderEntity.Product.Id, orderEntity.Product.ArticleNumber, orderEntity.Product.ProductName, orderEntity.Product.Price)));
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var orderEntity = await _context.Orders.FindAsync(id);
            if (orderEntity == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(orderEntity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderEntityExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}
