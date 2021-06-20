using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WhatsAppApi.Data;
using WhatsAppApi.Helpers;
using WhatsAppApi.Models;
using WhatsAppApi.Repositories;

namespace WhatsAppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserModelsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserRepository _repository;
        private readonly IJwtService _jwtService;

        public UserModelsController(IUserRepository repository, IJwtService jwtService, ApplicationDbContext context)
        {
            _repository = repository;
            _jwtService = jwtService;
            _context = context;

        }

        // GET: api/UserModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserModel>>> GetChatList()
        {
            try
            {
                //if (Request.Headers.ContainsKey("Authorization"))
                //{

                //  var jwt = Request.Headers[HeaderNames.Authorization];//Request.Cookies["jwt"];
                var jwt = Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);

                var token = _jwtService.Verify(jwt);

                var user_id = int.Parse(token.Issuer);

                var userList = _context.Users
                    .Where(u => u.Id == user_id)
                    .Include(r => r.OwnerRooms)
                    .ThenInclude(u => u.Messages)
                    .Include(r => r.Guest_Rooms)
                    .ThenInclude(m => m.Messages)
                    .ToList();

                return Ok(userList);
                //}
            }
            catch (Exception ex)
            {
                return Unauthorized(new { err = ex.Message });
            }
        }


        [HttpGet("search/{Username}")]
        public async Task<ActionResult<IEnumerable<UserModel>>> SearchForUsers(string Username)
        {
            try
            {
                var jwt = Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);

                var token = _jwtService.Verify(jwt);

                var user_id = int.Parse(token.Issuer);

                var userList = _context.Users
                    .Where(u=> u.Id != user_id)
                    .Where(u => u.Username.Contains(Username)).ToList();

                return Ok(userList);
                //}
            }
            catch (Exception ex)
            {
                return Unauthorized(new { err = ex.Message });
            }
        }
        // GET: api/UserModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserModel>> GetUserModel(int id)
        {
            
            var userModel = await _context.Users.FindAsync(id);

            if (userModel == null)
            {
                return NotFound();
            }

            return userModel;
        }

        // PUT: api/UserModels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserModel(int id, UserModel userModel)
        {
            if (id != userModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(userModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserModelExists(id))
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

        // POST: api/UserModels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserModel>> PostUserModel(UserModel userModel)
        {
            _context.Users.Add(userModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserModel", new { id = userModel.Id }, userModel);
        }

        // DELETE: api/UserModels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserModel(int id)
        {
            var userModel = await _context.Users.FindAsync(id);
            if (userModel == null)
            {
                return NotFound();
            }

            _context.Users.Remove(userModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserModelExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
