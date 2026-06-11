using Core.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Authentication;
using System.Security.Claims;

namespace API.Extensions
{
    public static class ClaimsPrincipleExtensions
    {
        public static async Task<AppUser> GetUserByEmail(this UserManager<AppUser> userManager,ClaimsPrincipal user)
        {
            var UserToReturn = userManager.Users
                .FirstOrDefault(x => x.Email == user.GetEmail());


            if (UserToReturn == null)
                throw new AuthenticationException("User not found!");

            return UserToReturn;
        }

        public static async Task<AppUser> GetUserByEmailWithAddress(this UserManager<AppUser> userManager, ClaimsPrincipal user)
        {
            var UserToReturn = userManager.Users.Include(x => x.Address)
                .FirstOrDefault(x => x.Email == user.GetEmail());


            if (UserToReturn == null)
                throw new AuthenticationException("User not found!");

            return UserToReturn;
        }

        public static string GetEmail(this ClaimsPrincipal user)
        {
            var email=user.FindFirstValue(ClaimTypes.Email)
                ??throw new AuthenticationException("Email claim was not found!");

            return email;
        }
    }
}
