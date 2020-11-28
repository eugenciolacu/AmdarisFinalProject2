﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using AmdarisInternship.API.Dtos.Account;
using AmdarisInternship.API.Infrastructure.Configurations;
using AmdarisInternship.API.Infrastructure.Models;
using AmdarisInternship.API.Services.Interfaces;
using AmdarisInternship.Domain.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AmdarisInternship.API.Services.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly AuthOptions _authenticationOptions;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public AccountService(IOptions<AuthOptions> authenticationOptions, UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _authenticationOptions = authenticationOptions.Value;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<JwtSecurityToken> Login(UserForLoginDto userForLoginDto)
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
                );

                return jwtSecurityToken;
            }

            return null;
        }

        public async Task<RegistrationResponse> Register (UserForRegisterDto model)
        {
            RegistrationResponse response;

            var userExists = await _userManager.FindByNameAsync(model.Email);

            if (userExists != null)
            {
                response = new RegistrationResponse { Status = "Error", Message = "User already exists!" };
                return await Task.FromResult(response);
            }

            User user = new User()
            {
                UserName = model.Email,
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                FirstName = model.FirstName,
                LastName = model.LastName,
                Skype = model.Skype,
                AvatarExtension = model.AvatarExtension,
                AvatarName = model.AvatarName,
                Avatar = model.Avatar
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if(!result.Succeeded)
            {
                response = new RegistrationResponse { Status = "Error", Message = "User creation failed! Please check user details and try again." };
                return await Task.FromResult(response);
            }

            if (await _roleManager.RoleExistsAsync(model.UserRole))
            {
                await _userManager.AddToRoleAsync(user, model.UserRole);
            }

            response = new RegistrationResponse { Status = "Success", Message = "User created successfully!" };
            return await Task.FromResult(response);
        }
    }
}
