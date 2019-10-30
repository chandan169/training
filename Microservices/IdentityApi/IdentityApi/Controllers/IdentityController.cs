﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IdentityApi.Infrastructure;
using IdentityApi.Models;
using IdentityApi.Models.ViewModel;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace IdentityApi.Controllers
{
    [Route("api/auth")]
    [ApiController]
    
    public class IdentityController : ControllerBase
    {
        private IdentityDBContext db;
        private IConfiguration config;

        public IdentityController(IdentityDBContext db, IConfiguration config)
        {
            this.db = db;
            this.config = config;
        }

        [HttpPost("register",Name ="RegisterUser")]
        public async Task<ActionResult<dynamic>>Register(User user)
        {
            TryValidateModel(user);
            if (ModelState.IsValid)
            {
                await db.Users.AddAsync(user);
                await db.SaveChangesAsync();
                return Created("", new
                {

                    user.Id,
                    user.Fullname,
                    user.Username,
                    user.Email
                });
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPost("token",Name ="GetToken")]
        public ActionResult<dynamic>GetToken(LoginModel model)
        {
            TryValidateModel(model);
            if (ModelState.IsValid)
            {
                var user = db.Users.SingleOrDefault(s => s.Username == model.Username && s.Password == model.Password);
                if (user != null)
                {
                    var token = GenerateToken(user);
                    return Ok(new { user.Fullname,user.Email,user.Username,Token = token});
                }
                else return Unauthorized();
            }
            else return BadRequest();
        }

        [NonAction]
        private string GenerateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Fullname),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };

            claims.Add(new Claim(JwtRegisteredClaimNames.Aud, "catalogapi"));
            claims.Add(new Claim(JwtRegisteredClaimNames.Aud, "paymentapi"));
            claims.Add(new Claim(JwtRegisteredClaimNames.Aud, "basketapi"));
            claims.Add(new Claim(JwtRegisteredClaimNames.Aud, "orderapi"));
            claims.Add(new Claim(ClaimTypes.Role, user.Role));
            
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetValue<string>("Jwt:secret")));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: config.GetValue<string>("Jwt:issuer"),
                audience: null,
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials:credentials
            );
            string tokenString = new JwtSecurityTokenHandler().WriteToken(token);
                return tokenString;
        }
    }
}