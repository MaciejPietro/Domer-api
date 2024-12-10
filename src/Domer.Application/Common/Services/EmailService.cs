using Domer.Domain.Common.Entities;
using Domer.Domain.Common.Interfaces;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Domer.Application.Common.Services;

public class EmailService : IEmailService
{
    
    private readonly IEmailSender _emailSender;
    
    public EmailService(IEmailSender emailSender)
    {
        _emailSender = emailSender;
    }
    public async Task<string> LoadEmailTemplateAsync(string templateName)
    {
        string basePath = AppContext.BaseDirectory;
        string templatePath = Path.Combine(basePath, "Email", "Templates", templateName);
    
        if (File.Exists(templatePath))
        {
            return await File.ReadAllTextAsync(templatePath);
        }

        throw new FileNotFoundException($"Email template {templateName} not found at {templatePath}.");
    }

    public async Task SendRegistrationConfirmationEmailAsync(string userEmail, string confirmationLink)
    {
        string emailTemplate = await LoadEmailTemplateAsync("ConfirmationEmail.html");
        
        string emailBody = emailTemplate
            .Replace("{{CONFIRMATION_LINK}}", confirmationLink)
            .Replace("{{USER_EMAIL}}", userEmail)
            .Replace("{{CURRENT_YEAR}}", DateTime.Now.Year.ToString());

        var message = new Message(
            new[] { userEmail }, 
            "Budoma - Potwierdź adres email", 
            emailBody
        );

        await _emailSender.SendEmailAsync(message);
    }
}