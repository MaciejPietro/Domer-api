using Domer.Application.Common.Exceptions;
using Domer.Application.Common.Interfaces;
using Domer.Domain.Common.Interfaces;
using Domer.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace Domer.Infrastructure.Services;
    
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailService _emailService;
        
        public IdentityService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IEmailService  emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;

        }
        
        public async Task<bool> SigninUserAsync(string emailAddress, string password)
        {
            SignInResult? result = await _signInManager.PasswordSignInAsync(emailAddress, password, false, false);
            return result.Succeeded;
        }
        
        public async Task<bool> IsUserExists(string emailAddress)
        {
            ApplicationUser? user = await _userManager.FindByEmailAsync(emailAddress!);
            
            return user != null;
        }
        
        public async Task<bool> HasConfirmedEmail(string emailAddress)
        {
            ApplicationUser? user = await _userManager.FindByEmailAsync(emailAddress!);
            
            if (user is null)
            {
                throw new BadRequestException("Użytkownik o podanym adresie email nie istnieje.");
            }
            
            return await _userManager.IsEmailConfirmedAsync(user);
        }

        public async Task<IApplicationUser> GetUserAsync(ClaimsPrincipal principal)
        {
            ApplicationUser? currentUser = await _userManager.GetUserAsync(principal);

            return currentUser;
        }

        public async Task<bool> CheckPasswordAsync(IApplicationUser user, string password)
        {
            bool isCurrentPasswordCorrect = await _userManager.CheckPasswordAsync((ApplicationUser) user, password);
            
            return isCurrentPasswordCorrect;
        }
        
        public async Task<IApplicationUser> GetUserDetailsByEmailAsync(string emailAddress)
        {
            ApplicationUser? user = await _userManager.FindByEmailAsync(emailAddress);
            if (user == null) throw new NotFoundException("Użytkownik o podanym adresie email nie istnieje");

            return user;
        }
        
        public async Task<IApplicationUser> GetUserDetailsAsync(string userId)
        {
     
            
            ApplicationUser? user = await _userManager.FindByIdAsync(userId);
            
            if (user == null) throw new NotFoundException($"Użytkownik o id {userId} nie istnieje");

            return user;
            
          
        }
        
        
        public async Task LogoutUserAsync()
        {
            await _signInManager.SignOutAsync().ConfigureAwait(false);
        }

        public async Task<IdentityResult> ResetPasswordAsync(string emailAddress, string token, string newPassword)
        {
            ApplicationUser? user = await _userManager.FindByEmailAsync(emailAddress);

            return await _userManager.ResetPasswordAsync(user!, token, newPassword);
        }

        public async Task<IApplicationUser> CreateUserAsync(string addressEmail, string password)
        {

            ApplicationUser user = new() { UserName = addressEmail, Email = addressEmail };
            
            IdentityResult result = await _userManager.CreateAsync(user, password);
        
            if (!result.Succeeded)
            {
                throw new ValidationException(result.Errors);
            }
        
        
            return user;
        }

        public async Task<string> GenerateEmailConfirmationTokenAsync(IApplicationUser user)
        {
            
            string token = await _userManager.GenerateEmailConfirmationTokenAsync((ApplicationUser)user);
            
            return WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
        }

        public async Task SendConfirmationEmail(string clientUri, string emailAddress, string token)
        {
            Dictionary<string, string?> param = new()
            { 
                { "token", token }, 
                { "email", emailAddress } 
            };
        
            string callbackLink = QueryHelpers.AddQueryString(clientUri!, param);
        
            await _emailService.SendRegistrationConfirmationEmailAsync(emailAddress, callbackLink);
        }

        public async Task ConfirmUserEmail(string emailAddress, string token)
        {
            string decodedToken = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));

            ApplicationUser? user = await _userManager.FindByEmailAsync(emailAddress);
    
            await _userManager.ConfirmEmailAsync(user!, decodedToken);
        }

        public async Task UpdateUserProfile(IApplicationUser user, string newEmail)
        {
    
            
            IdentityResult setEmailResult = await _userManager.SetEmailAsync((ApplicationUser)user, newEmail);
            IdentityResult setUserNameResult = await _userManager.SetUserNameAsync((ApplicationUser)user, newEmail);

            
            if (!setEmailResult.Succeeded || !setUserNameResult.Succeeded)
            {
                throw new BadRequestException("Nie udało się zaktualizować użytkownika");
            }
        }
        
        public async Task UpdateUserPassword(string userId, string password, string currentPasword)
        {
            ApplicationUser? user = await _userManager.FindByIdAsync(userId);
            

            bool isCurrentPasswordCorrect = await _userManager.CheckPasswordAsync(user!, currentPasword);
    
            if (!isCurrentPasswordCorrect)
            {
                throw new BadRequestException("Obecne hasło jest nieprawidłowe");
            }
            
            string resetToken = await _userManager.GeneratePasswordResetTokenAsync(user!);
        
            IdentityResult resetPassResult = await _userManager.ResetPasswordAsync(user!, resetToken, password);
        
            if (!resetPassResult.Succeeded)
            {
                throw new BadRequestException("Zmiana hasła nie udała się");
            }
        }


        public async Task<bool> DeleteUserAsync(IApplicationUser user)
        {
            try
            {
                IdentityResult deleteResult = await _userManager.DeleteAsync((ApplicationUser)user);

                return true;
            }
            catch (Exception err)
            {
                throw new InternalException("Coś poszło nie tak", err);
            }
            
            
        }

        public async Task<bool> SingOutUser()
        {
            try
            {
                await _signInManager.SignOutAsync();

                return true;
            }
            catch (Exception err)
            {
                throw new InternalException("Coś poszło nie tak", err);
            }
        }



     

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
