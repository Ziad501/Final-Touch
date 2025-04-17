using FinalTouch.Api.Dtos;
using FinalTouch.Api.Extensions;
using FinalTouch.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FinalTouch.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController(SignInManager<AppUser> signInManager) : BaseApiController
    {
        [HttpPost("Register")] 
        public async Task<ActionResult> Register(RegisterDto registerDto)
        {
            var user = new AppUser
            {
                UserName = registerDto.Email,
                Email = registerDto.Email,
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName
            };
            var result = await signInManager.UserManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
                return ValidationProblem();
            }
            return Ok();
        }
        [Authorize]
        [HttpPost("logout")]
        public async Task<ActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return NoContent();
        }

        [HttpGet("user-info")]
        public async Task<ActionResult<AppUser>> GetUserInfo()
        {
            if(User.Identity?.IsAuthenticated == false)
            {
                return NoContent();
            }
            var user = await signInManager.UserManager.GetUSerByEmailWithAddress(User);
            return Ok(new
            {
                user.FirstName,
                user.LastName,
                user.Email,
                Address = user.Address?.ToDto()
            });
        }   
        [HttpGet("auth-status")]
        public ActionResult GetAuthState()
        {
            return Ok(new { isAuthenticated = User.Identity?.IsAuthenticated ?? false });
        }
        [Authorize]
        [HttpPost("address")]
        public async Task<ActionResult<Address>> CreateOrUpdateAddress(AddressDto addressDto)
        {
            var user = await signInManager.UserManager.GetUSerByEmailWithAddress(User);
            if (user.Address == null)
            {
                user.Address = addressDto.ToEntity();
            }
            else
            {
                user.Address.UpdateFromDto(addressDto);
            }
            var result = await signInManager.UserManager.UpdateAsync(user);
            if (!result.Succeeded) return BadRequest("Problem updating the user address");
           return Ok(user.Address.ToDto());
        }
    }
}
