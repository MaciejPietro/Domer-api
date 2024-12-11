using Domer.Application.Common.Exceptions;
using Domer.Application.Common.Interfaces;
using Domer.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Domer.Infrastructure.Services;
    
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public IdentityService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        
        public async Task<bool> SigninUserAsync(string email, string password)
        {
            SignInResult? result = await _signInManager.PasswordSignInAsync(email, password, false, false);
            return result.Succeeded;
        }
        
        public async Task<bool> IsUserExists(string email)
        {
            ApplicationUser? user = await _userManager.FindByEmailAsync(email!);
            
            return user != null;
        }
        
        public async Task<IApplicationUser> GetUserDetailsAsync(string emailAddress)
        {
            ApplicationUser? user = await _userManager.FindByEmailAsync(emailAddress);
            if (user == null) throw new NotFoundException("User not found");

            return user;
        }
        
        
        public async Task LogoutUserAsync()
        {
            await _signInManager.SignOutAsync().ConfigureAwait(false);
        }

        // public async Task<(bool isSucceed, string userId)> CreateUserAsync(string userName, string password, string email, string fullName, List<string> roles)
        // {
        //     var user = new ApplicationUser()
        //     {
        //         FullName = fullName,
        //         UserName = userName,
        //         Email = email
        //     };
        //
        //     var result = await _userManager.CreateAsync(user, password);
        //
        //     if (!result.Succeeded)
        //     {
        //         throw new ValidationException(result.Errors);
        //     }
        //
        //     var addUserRole = await _userManager.AddToRolesAsync(user, roles);
        //     if (!addUserRole.Succeeded)
        //     {
        //         throw new ValidationException(addUserRole.Errors);
        //     }
        //     return (result.Succeeded, user.Id);
        // }


        // public async Task<bool> DeleteUserAsync(string userId)
        // {
        //     var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);
        //     if (user == null)
        //     {
        //         throw new NotFoundException("User not found");
        //         //throw new Exception("User not found");
        //     }
        //
        //     if (user.UserName == "system" || user.UserName == "admin")
        //     {
        //         throw new Exception("You can not delete system or admin user");
        //         //throw new BadRequestException("You can not delete system or admin user");
        //     }
        //     var result = await _userManager.DeleteAsync(user);
        //     return result.Succeeded;
        // }



     

        // public async Task<(string userId, string fullName, string UserName, string email, IList<string> roles)> GetUserDetailsByUserNameAsync(string userName)
        // {
        //     var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == userName);
        //     if (user == null)
        //     {
        //         throw new NotFoundException("User not found");
        //     }
        //     var roles = await _userManager.GetRolesAsync(user);
        //     return (user.Id, user.FullName, user.UserName, user.Email, roles);
        // }



        // public async Task<bool> IsUniqueUserName(string userName)
        // {
        //     return await _userManager.FindByNameAsync(userName) == null;
        // }

        // public async Task<bool> SigninUserAsync(string userName, string password)
        // {
        //     var result = await _signInManager.PasswordSignInAsync(userName, password, true, false);
        //     return result.Succeeded;
        //
        //
        // }
        //
        // public async Task<bool> UpdateUserProfile(string id, string fullName, string email, IList<string> roles)
        // {
        //     var user = await _userManager.FindByIdAsync(id);
        //     user.FullName = fullName;
        //     user.Email = email;
        //     var result = await _userManager.UpdateAsync(user);
        //
        //     return result.Succeeded;
        // }



   
    }
