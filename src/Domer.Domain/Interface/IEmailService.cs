using System.Runtime.InteropServices.JavaScript;
using System.Threading.Tasks;

namespace Domer.Domain.Interface;

public interface IEmailService
{
    Task<string> LoadEmailTemplateAsync(string templateName);
    Task SendRegistrationConfirmationEmailAsync(string userEmail, string confirmationLink);
}