using Domer.Domain;
using Domer.Infrastructure.Services;
using MimeKit;
using System;
using System.Threading.Tasks;

namespace Domer.Infrastructure.Services;

public interface IEmailSender
{
     // void SendEmail(Message message);
     Task SendEmailAsync(Message message);
}

