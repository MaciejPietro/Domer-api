using Domer.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Domer.Application.Common.Interfaces;

    public interface IIdentityService
    {
        // User section
        Task<IApplicationUser> CreateUserAsync(string email, string password);
        Task<bool> SigninUserAsync(string emailAddress, string password);
        Task<bool> IsUserExists(string emailAddress);

        Task<bool> HasConfirmedEmail(string emailAddress);

        Task<string> GenerateEmailConfirmationTokenAsync(IApplicationUser user);
        
        Task ConfirmUserEmail(string emailAddress, string token);
        Task<IApplicationUser> GetUserDetailsByEmailAsync(string emailAddress);
        Task<IApplicationUser> GetUserDetailsAsync(string userId);
        Task<IApplicationUser> GetUserAsync(ClaimsPrincipal principal);
        
        Task LogoutUserAsync();
        
        Task<IdentityResult> ResetPasswordAsync(string emailAddress, string token, string newPassword);
 
        Task<bool> CheckPasswordAsync(IApplicationUser user, string password);

        Task SendConfirmationEmail(string clientUri, string emailAddress, string token);
        
        Task UpdateUserProfile(IApplicationUser user, string newEmail); 

        Task UpdateUserPassword(string userId, string password, string currentPassword); 
        
        Task<bool> DeleteUserAsync(IApplicationUser user);
        
        Task<bool> SingOutUser();

        
        // Task<string> GetUserIdAsync(string userName);
        // Task<(string userId, string fullName, string UserName, string email, IList<string> roles)> GetUserDetailsByUserNameAsync(string userName);
        // Task<string> GetUserNameAsync(string userId);
        // Task<bool> DeleteUserAsync(string userId);
        // Task<bool> IsUniqueUserName(string userName);
        // Task<List<(string id, string fullName, string userName, string email)>> GetAllUsersAsync();
        // Task<List<(string id, string userName, string email, IList<string> roles)>> GetAllUsersDetailsAsync();
        // Task<bool> UpdateUserProfile(string id, string fullName, string email, IList<string> roles);

        // // Role Section
        // Task<bool> CreateRoleAsync(string roleName);
        // Task<bool> DeleteRoleAsync(string roleId);
        // Task<List<(string id, string roleName)>> GetRolesAsync();
        // Task<(string id, string roleName)> GetRoleByIdAsync(string id);
        // Task<bool> UpdateRole(string id, string roleName);
        //
        // // User's Role section
        // Task<bool> IsInRoleAsync(string userId, string role);
        // Task<List<string>> GetUserRolesAsync(string userId);
        // Task<bool> AssignUserToRole(string userName, IList<string> roles);
        // Task<bool> UpdateUsersRole(string userName, IList<string> usersRole);


    }
