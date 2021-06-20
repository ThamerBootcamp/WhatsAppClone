using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using WhatsAppApi.Dtos;
using WhatsAppApi.Helpers;
using WhatsAppApi.Models;
using WhatsAppApi.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WhatsAppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IUserRepository _repository;
        private readonly IJwtService _jwtService;

        public AuthController(IUserRepository repository, IJwtService jwtService)
        {
            _repository = repository;
            _jwtService = jwtService;
        }
        //[HttpGet]
        //public IActionResult Get()
        //{
        //    return Ok("success");
        //}

        // POST api/values
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var user = new UserModel
            {
                Username = dto.Username,
                Displayname = dto.Displayname,
                Img = dto.Img,
                Password = BCrypt.Net.BCrypt.HashPassword(dto.Password)
            };
            //return Ok("abdu");
            return Created("Success", await _repository.Create(user));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(RegisterDto dto)
        {
            var user = await _repository.GetUserByUsername(dto.Username);

            if (user == null)
                return BadRequest(new { message = "Invalid Email Address!" });

            if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
                return BadRequest(new { message = "Invalid Credentials!" });

            var jwt = _jwtService.Generate(user.Id);

            Response.Headers["Authorization"] = jwt;
           // Response.Cookies.Append("jwt", jwt, new CookieOptions { HttpOnly = true });

            return Ok(new {
                message ="Success",
                id = user.Id,
                token = jwt
            });
        }

        [HttpGet("user")]
        public async Task<IActionResult> User()
        {
            try
            {
                //if (Request.Headers.ContainsKey("Authorization"))
                //{

                //  var jwt = Request.Headers[HeaderNames.Authorization];//Request.Cookies["jwt"];
                    var jwt = Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);

                    var token = _jwtService.Verify(jwt);

                    var user_id = int.Parse(token.Issuer);

                    var user = await _repository.GetUserById(user_id);
                

                    return Ok(user);
                //}
            }
            catch (Exception ex)
            {
                return Unauthorized(new {err=ex.Message });
            }

        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwt");
            return Ok(new { message = "success" });
        }
    }
}
