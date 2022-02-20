#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi;
using WebApi.Filters;
using WebApi.Models;
using WebApi.Models.Entities;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [ApiKey]

    public class UsersController : ControllerBase
    {
        private readonly SqlContext _context;

        public UsersController(SqlContext context)
        {
            _context = context;
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserOutputModel>>> GetUsers()
        {
            var items = new List<UserOutputModel>();
            foreach (var i in await _context.Users.Include(x => x.Address).ToListAsync())
                items.Add(new UserOutputModel(i.Id, i.FirstName, i.LastName, i.Email, new AddressModel(i.Address.StreetName, i.Address.PostalCode, i.Address.City)));
            return items;
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<UserOutputModel>> GetUser(int id)
        {
            var userEntity = await _context.Users.Include(x => x.Address).FirstOrDefaultAsync(x => x.Id == id);

            if (userEntity == null)
            {
                return NotFound();
            }

            return new UserOutputModel(userEntity.Id, userEntity.FirstName, userEntity.LastName, userEntity.Email, 
                   new AddressModel(userEntity.Address.StreetName, userEntity.Address.PostalCode, userEntity.Address.City));
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserUpdateModel userUpdateModel) {


            if (id != userUpdateModel.Id) {
                return BadRequest();
            }

            var userEntity = await _context.Users.FindAsync(userUpdateModel.Id);

            userEntity.FirstName = userUpdateModel.FirstName;
            userEntity.LastName = userUpdateModel.LastName;
            userEntity.Email = userUpdateModel.Email;

            _context.Entry(userEntity).State = EntityState.Modified;

            try {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) {
                if (!UserEntityExists(id)) {
                    return NotFound();
                }
                else {
                    throw;
                }
            }

            return NoContent();
        }


        [HttpPost]
        public async Task<ActionResult<UserOutputModel>> PostUser(UserInputModel model)
        {
            if (await _context.Users.AnyAsync(x => x.Email == model.Email))
                return BadRequest();

            var userEntity = new UserEntity(model.FirstName, model.LastName, model.Email);

            var address = await _context.Addresses.FirstOrDefaultAsync(x => x.StreetName == model.StreetName && x.PostalCode == model.PostalCode);
            if (address != null)
                userEntity.AddressId = address.Id;
            else
                userEntity.Address = new AddressEntity(model.StreetName, model.PostalCode, model.City);

            _context.Users.Add(userEntity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = userEntity.Id }, 
                new UserOutputModel(userEntity.Id, userEntity.FirstName, userEntity.LastName, userEntity.Email,
                new AddressModel(userEntity.Address.StreetName, userEntity.Address.PostalCode, userEntity.Address.City)));
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id) {

            var userEntity = await _context.Users.FindAsync(id);

            if (userEntity == null) {
                return NotFound();
            }

            _context.Users.Remove(userEntity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserEntityExists(int id) {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
