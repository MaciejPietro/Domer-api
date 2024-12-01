using MimeKit;
using MailKit.Net.Smtp;
using System;
using System.Threading.Tasks;

namespace Domer.Infrastructure.Services;

public class EmailSender : IEmailSender
{
    private readonly EmailConfiguration _emailConfig;

    public EmailSender(EmailConfiguration emailConfig)
    {
        _emailConfig = emailConfig;
    }

    public async Task SendEmailAsync(Message message)
    {
        var emailMessage = CreateEmailMessage(message);
       
        SendAsync(emailMessage);
    }

    private MimeMessage CreateEmailMessage(Message message)
    {
        var emailMessage = new MimeMessage();
        emailMessage.From.Add(new MailboxAddress(_emailConfig.From, _emailConfig.From));
        emailMessage.To.AddRange(message.To);
        emailMessage.Subject = message.Subject;
        emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = message.Content };

        return emailMessage;
    }

    private async void SendAsync(MimeMessage mailMessage)
    {
        using (var client = new SmtpClient())
        {
            try
            {

                Console.WriteLine(_emailConfig.SmtpServer);
                Console.WriteLine(_emailConfig.Port);

                // Connect to the SMTP server
                await client.ConnectAsync(_emailConfig.SmtpServer, _emailConfig.Port, MailKit.Security.SecureSocketOptions.StartTls);
                
                // Remove XOAUTH2 authentication mechanism if not using OAuth
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                
                // Authenticate
                await client.AuthenticateAsync(_emailConfig.UserName, _emailConfig.Password);

                // Send the email
                var res = await client.SendAsync(mailMessage);
                
                Console.WriteLine(res);
            }
            catch (Exception ex)
            {
                // Consider logging the exception instead of just writing to console
                Console.WriteLine($"Email sending failed: {ex.Message}");
                throw;
            }
            finally
            {
                // Disconnect from the server
                await client.DisconnectAsync(true);
            }
        }
    }
}