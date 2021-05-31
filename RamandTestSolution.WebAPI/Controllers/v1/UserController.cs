using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RamandTestSolution.Application.Features.User.Commands;
using RamandTestSolution.Application.Features.User.DTOs;
using RamandTestSolution.Application.Features.User.Queries;

namespace RamandTestSolution.WebAPI.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class UserController : ApiController
    {
        private IConfiguration _config;
        public UserController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<string>> Login(Application.Features.User.Commands.Login login)
        {
            var User = await Mediator.Send(login);
            if (User == null)
                return StatusCode(401);
            return Ok(new { token = Authenticate(User) });
        }
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<Domain.Entities.Users>>> GetAllUser()
        {
            return await Mediator.Send(new GetAllUser());
        }
        private string Authenticate(LoginOutputDTO user)
        {

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Jti, user.ID.ToString())
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              claims,
              expires: DateTime.Now.AddMinutes(30),
              signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
