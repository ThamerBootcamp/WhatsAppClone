using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatsAppApi.Data;
using WhatsAppApi.Helpers;
using WhatsAppApi.Models;
using WhatsAppApi.Repositories;

namespace WhatsAppApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {

        private readonly ApplicationDbContext _context;
        private readonly IRoomRepository _repository;
        private readonly IJwtService _jwtService;

        public RoomController(IRoomRepository repository, IJwtService jwtService, ApplicationDbContext context)
        {
            _repository = repository;
            _jwtService = jwtService;
            _context = context;

        }
        [HttpPost("Createroom")]
        public async Task<IActionResult> Register(RoomDto dto)
        {
            try
            {
                var jwt = Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);

                var token = _jwtService.Verify(jwt);
                var user_id = int.Parse(token.Issuer);

                var user = await _context.Users.Include(r => r.OwnerRooms).Include(r => r.Guest_Rooms).FirstAsync(u => u.Id == user_id);
                var guest = await _context.Users.Include(r => r.OwnerRooms).Include(r => r.Guest_Rooms).FirstAsync(u => u.Id == dto.GuestID);

                //if exists return room 
                var room = user.OwnerRooms.FirstOrDefault(i => i.Guest == guest);
                var room2 = user.Guest_Rooms.FirstOrDefault(i => i.Guest == guest);

                if (room != null)
                {
                    return Ok(room);
                }
                else if (room2 != null)
                {
                    return Ok(room2);
                }
                else
                {
                //else create one 
                    room = new RoomModel
                    {
                        Name = Guid.NewGuid().ToString(),
                        Owner = user,
                        Guest= guest 
                    };
                
                    return Created("Success", await _repository.Create(room));
                }

            }
            catch (Exception)
            {
                Unauthorized();
                throw;
            }
            //return Ok("abdu");
        }

        //[HttpPost("Createroom")]
        //public async Task<IActionResult> CreateRoom(RoomDto gid)
        //{
        //    try
        //    {
        //        //if (Request.Headers.ContainsKey("Authorization"))
        //        //{

        //        //  var jwt = Request.Headers[HeaderNames.Authorization];//Request.Cookies["jwt"];
        //        var jwt = Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);

        //        var token = _jwtService.Verify(jwt);
        //        var user_id = int.Parse(token.Issuer);
        //        var guser = _context.Users.Include(r=>r.OwnerRooms).Include(r=>r.Guest_Rooms).ToList();
        //        //var user = _context.Users.ToList();


        //        /*if (users.Count == 2)
        //        {*/
        //            var owner = guser.Find(u => u.Id == user_id);
        //            var guest = guser.Find(u => u.Id == gid.GuestID);

        //        /*if (owner.OwnerRooms.Find(r=>r.Guest.Id==guest_id)!=null||
        //                owner.Guest_Rooms.Find(r=>r.Guest.Id==guest_id)!=null
        //               )
        //        {*/
        //                var room = new RoomModel
        //                {
        //                    Name = Guid.NewGuid().ToString(),
        //                    Owner = owner,
        //                    Guest = guest
        //                };
        //        //owner.OwnerRooms.Add(room);
        //        //guest.Guest_Rooms.Add(room);

        //         //_context.Rooms.Add(room);
        //         //await _context.SaveChangesAsync();
        //            return Ok(room);
        //            //}
        //        //}
        //        //return BadRequest();

        //        /*var userList = _context.Users
        //            .Where(u => u.Id != user_id)
        //            .Include(r => r.OwnerRooms)
        //            .ThenInclude(u => u.Messages)
        //            .Include(r => r.Guest_Rooms)
        //            .ThenInclude(m => m.Messages)
        //            .ToList();*/

        //        //}
        //    }
        //    catch (Exception ex)
        //    {
        //        return Unauthorized(new { err = ex.Message });
        //    }
        //}
    }
    public class RoomDto
    {
        public int GuestID { get; set; }
    }
}
