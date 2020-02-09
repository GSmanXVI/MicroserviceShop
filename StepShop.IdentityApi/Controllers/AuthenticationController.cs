using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StepShop.IdentityApi.Data;
using StepShop.IdentityApi.DTO;
using StepShop.IdentityApi.Models;
using StepShop.IdentityApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StepShop.IdentityApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<AppUser> userManager;
        private readonly TokenGenerator tokenGenerator;
        private readonly AccountsDbContext accountsDbContext;

        public AuthenticationController(
            UserManager<AppUser> userManager,
            TokenGenerator tokenGenerator,
            AccountsDbContext accountsDbContext)
        {
            this.userManager = userManager;
            this.tokenGenerator = tokenGenerator;
            this.accountsDbContext = accountsDbContext;
        }

        [Authorize]
        [HttpGet("ping")]
        public ActionResult<string> Ping()
        {            
            return HttpContext.User.Identity.Name;
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthenticationResponseDto>> LoginAsync(LoginCredentialsDto dto)
        {
            var user = await userManager.FindByNameAsync(dto.Email);

            if (user == null)
                return BadRequest();

            if (!await userManager.CheckPasswordAsync(user, dto.Password))
                return BadRequest();

            var accessToken = tokenGenerator.GenerateAccessToken(user);
            var refreshToken = tokenGenerator.GenerateRefreshToken();

            accountsDbContext.RefreshTokens.Add(new RefreshToken
            {
                Token = refreshToken,
                ExpiresAt = DateTime.Now.Add(tokenGenerator.Options.RefreshExpiration),
                AppUserId = user.Id
            });
            await accountsDbContext.SaveChangesAsync();

            var response = new AuthenticationResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
            return response;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RefreshAsync(LoginCredentialsDto dto)
        {
            var user = new AppUser
            {
                Email = dto.Email,
                UserName = dto.Email
            };
            var result = await userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
                return BadRequest();

            return Ok();
        }

        [HttpGet("refresh/{oldRefreshToken}")]
        public async Task<ActionResult<AuthenticationResponseDto>> RefreshAsync(string oldRefreshToken)
        {
            var token = await accountsDbContext.RefreshTokens.FindAsync(oldRefreshToken);

            if (token == null)
                return BadRequest();

            accountsDbContext.RefreshTokens.Remove(token);

            if (token.ExpiresAt < DateTime.Now)
                return BadRequest();

            var user = await userManager.FindByIdAsync(token.AppUserId);

            var accessToken = tokenGenerator.GenerateAccessToken(user);
            var refreshToken = tokenGenerator.GenerateRefreshToken();

            accountsDbContext.RefreshTokens.Add(new RefreshToken
            {
                Token = refreshToken,
                ExpiresAt = DateTime.Now.Add(tokenGenerator.Options.RefreshExpiration),
                AppUserId = user.Id
            });
            await accountsDbContext.SaveChangesAsync();

            var response = new AuthenticationResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
            return response;
        }

        [HttpGet("logout/{refreshToken}")]
        public async Task<IActionResult> LogoutAsync(string refreshToken)
        {
            var token = accountsDbContext.RefreshTokens.Find(refreshToken);
            if (token != null)
            {
                accountsDbContext.RefreshTokens.Remove(token);
                await accountsDbContext.SaveChangesAsync();
            }
            return NoContent();
        }
    }
}
