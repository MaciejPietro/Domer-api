using Domer.Domain;
using MimeKit;
using System;
using System.Threading.Tasks;
using Domer.Domain.Entities;

namespace Domer.Domain.Interface;

public interface IEmailSender
{
     Task SendEmailAsync(Message message);
}

