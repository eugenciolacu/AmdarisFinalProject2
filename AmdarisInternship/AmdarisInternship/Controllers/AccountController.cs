using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AmdarisInternship.API.Dtos.Account;
using AmdarisInternship.API.Infrastructure.Configurations;
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
        private readonly AuthOptions _authenticationOptions;
        private readonly SignInManager<User> _signInManager;

        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public AccountController(IOptions<AuthOptions> authenticationOptions, SignInManager<User> signInManager, UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _authenticationOptions = authenticationOptions.Value;
            _signInManager = signInManager;

            _userManager = userManager;
            _roleManager = roleManager;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
        {
            var user = await _userManager.FindByNameAsync(userForLoginDto.Username);
            if (user != null && await _userManager.CheckPasswordAsync(user, userForLoginDto.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var signinCredentials = new SigningCredentials(_authenticationOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256);
                var jwtSecurityToken = new JwtSecurityToken(
                     issuer: _authenticationOptions.Issuer,
                     audience: _authenticationOptions.Audience,
                     claims: authClaims,
                     expires: DateTime.Now.AddDays(30),
                     signingCredentials: signinCredentials
                ) ;

                var tokenHandler = new JwtSecurityTokenHandler();

                var encodedToken = tokenHandler.WriteToken(jwtSecurityToken);

                return Ok(new { AccessToken = encodedToken });
            }

            return Unauthorized();
        }
    }
}
