using Domer.Domain.Entities;
using Domer.Domain.Interface;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Domer.Infrastructure.Services;

public class EmailService(IEmailSender emailSender) : IEmailService
{
    public async Task<string> LoadEmailTemplateAsync(string templateName)
    {
        string basePath = AppContext.BaseDirectory;
        string templatePath = Path.Combine(basePath, "EmailTemplates", templateName);
    
        if (File.Exists(templatePath))
        {
            return await File.ReadAllTextAsync(templatePath);
        }

        throw new FileNotFoundException($"Email template {templateName} not found at {templatePath}.");
    }

    public async Task SendRegistrationConfirmationEmailAsync(string userEmail, string confirmationLink)
    {
        // Load the template
        string emailTemplate = await LoadEmailTemplateAsync("ConfirmationEmail.html");
        
        // Replace placeholders
        string emailBody = emailTemplate
            .Replace("{{CONFIRMATION_LINK}}", confirmationLink)
            .Replace("{{USER_EMAIL}}", userEmail)
            .Replace("{{CURRENT_YEAR}}", DateTime.Now.Year.ToString());

        var message = new Message(
            new[] { userEmail }, 
            "Budoma - Potwierdź adres email", 
            emailBody
        );

        await emailSender.SendEmailAsync(message);
    }
}