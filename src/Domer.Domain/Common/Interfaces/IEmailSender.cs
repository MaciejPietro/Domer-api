using Domer.Domain;
using MimeKit;
using System;
using System.Threading.Tasks;
using Domer.Domain.Common.Entities;

namespace Domer.Domain.Common.Interfaces;

public interface IEmailSender
{
     Task SendEmailAsync(Message message);
}

