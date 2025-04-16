using System;
using System.Security.Authentication;
using System.Security.Claims;
using FinalTouch.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FinalTouch.Api.Extensions;

public static class ClaimsPrincipleExtensions
{
    public static async Task<AppUser> GetUSerByEmail(this UserManager<AppUser> userManager, ClaimsPrincipal user)
    {
        var userToReturn = await userManager.Users.FirstOrDefaultAsync(x => x.Email == user.GetEmail()) ?? throw new AuthenticationException("User not found.");
        return userToReturn;
    }
    public static async Task<AppUser> GetUSerByEmailWithAddress(this UserManager<AppUser> userManager, ClaimsPrincipal user)
    {
        var userToReturn = await userManager.Users.Include(x=>x.Address).FirstOrDefaultAsync(x => x.Email == user.GetEmail()) ?? throw new AuthenticationException("User not found.");
        return userToReturn;
    }
    public static string GetEmail(this ClaimsPrincipal user)
    {
        return user.FindFirstValue(ClaimTypes.Email) ?? throw new AuthenticationException("Email claim not found.");
    }
}
