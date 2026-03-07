using System.Runtime.InteropServices.JavaScript;
using System.Threading.Tasks;

namespace Kompass.Domain.Common.Interfaces;

public interface IEmailService
{
    Task<string> LoadEmailTemplateAsync(string templateName);
    Task SendRegistrationConfirmationEmailAsync(string userEmail, string confirmationLink);
    
    
    Task SendResetPasswordEmailAsync(string userEmail, string resetPasswordLink);
}