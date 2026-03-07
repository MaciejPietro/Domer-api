using Kompass.Domain;
using MimeKit;
using System;
using System.Threading.Tasks;
using Kompass.Domain.Common.Entities;

namespace Kompass.Domain.Common.Interfaces;

public interface IEmailSender
{
     Task SendEmailAsync(Message message);
}

