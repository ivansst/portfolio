﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Portfolio.Server.Data.Models;
using Portfolio.Server.Models.Identity;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Server.Controllers
{
  public class IdentityController : ApiController
  {
    private readonly UserManager<User> userManager;
    private readonly AppSettings appSettings;

    public IdentityController(UserManager<User> userManager,
                                 IOptions<AppSettings> appSettings)
    {
      this.userManager = userManager;
      this.appSettings = appSettings.Value;
    }

    [HttpPost]
    [AllowAnonymous]
    [Route(nameof(Register))]
    public async Task<ActionResult> Register(RegisterRequestModel model)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState.Values);
      }

      var user = new User
      {
        Email = model.Email,
        UserName = model.UserName
      };

      var result = await this.userManager.CreateAsync(user, model.Password);

      if (!result.Succeeded)
      {
        return BadRequest(result.Errors);
      }

      return Ok();
    }

    [HttpPost]
    [AllowAnonymous]
    [Route(nameof(Login))]
    public async Task<ActionResult<string>> Login(LoginRequestModel model)
    {
      var user = await this.userManager.FindByNameAsync(model.UserName);
      if (user == null)
      {
        return Unauthorized();
      }

      var passwordValid = await this.userManager.CheckPasswordAsync(user, model.Password);

      if (!passwordValid)
      {
        return Unauthorized();
      }

      var tokenHandler = new JwtSecurityTokenHandler();
      var key = Encoding.ASCII.GetBytes(this.appSettings.Secret);

      var tokenDescriptor = new SecurityTokenDescriptor
      {
        Subject = new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.UserName)
        }),
        Expires = DateTime.UtcNow.AddDays(7),
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
      };

      var token = tokenHandler.CreateToken(tokenDescriptor);
      var encryptedToken = tokenHandler.WriteToken(token);

      return encryptedToken;
    }

    [HttpGet]
    [Route(nameof(GetUserId))]
    public ActionResult<string> GetUserId()
    {
      var userId = this.UserId;

      return userId;
    }
  }
}
