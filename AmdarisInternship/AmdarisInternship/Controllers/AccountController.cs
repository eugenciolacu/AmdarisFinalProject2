using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AmdarisInternship.API.Dtos.Account;
using AmdarisInternship.API.Infrastructure.Configurations;
using AmdarisInternship.API.Infrastructure.Models;
using AmdarisInternship.API.Services.Interfaces;
using AmdarisInternship.Domain.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AmdarisInternship.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
        {
            var result = await _accountService.Login(userForLoginDto);

            if (result != null)
            {
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(result),
                    expiration = result.ValidTo
                });
            }
            else
            {
                return Unauthorized();
            }
        }

        [Authorize(Roles = UserRoles.Administrator)]
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] UserForRegisterDto userForRegisterDto)
        {
            RegistrationResponse response = await _accountService.Register(userForRegisterDto);

            if (response.Status == "Error")
            {
                return Unauthorized(response.Message);
            }
            else
            {
                return Ok();
            }
        }
    }
}
