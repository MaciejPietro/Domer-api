using Domer.Domain.Common.Entities;
using Domer.Domain.Common.Interfaces;
using MimeKit;
using MailKit.Net.Smtp;
using System;
using System.Threading.Tasks;

namespace Domer.Infrastructure.Email;

public class EmailSender(EmailConfiguration emailConfig) : IEmailSender
{
    public async Task SendEmailAsync(Message message)
    {
        var emailMessage = CreateEmailMessage(message);
       
        SendAsync(emailMessage);
    }

    private MimeMessage CreateEmailMessage(Message message)
    {
        var emailMessage = new MimeMessage();
        emailMessage.From.Add(new MailboxAddress(emailConfig.From, emailConfig.From));
        emailMessage.To.AddRange(message.To);
        emailMessage.Subject = message.Subject;
        emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = message.Content };

        return emailMessage;
    }

    private async void SendAsync(MimeMessage mailMessage)
    {
        using (var client = new SmtpClient())
        {
            try
            {
        
                await client.ConnectAsync(emailConfig.SmtpServer, emailConfig.Port, MailKit.Security.SecureSocketOptions.StartTls);
                
                client.AuthenticationMechanisms.Remove("XOAUTH2");
         
                await client.AuthenticateAsync(emailConfig.UserName, emailConfig.Password);

                string? res = await client.SendAsync(mailMessage);
                
                Console.WriteLine(res);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Email sending failed: {ex.Message}");
                throw;
            }
            finally
            {
                await client.DisconnectAsync(true);
            }
        }
    }
}