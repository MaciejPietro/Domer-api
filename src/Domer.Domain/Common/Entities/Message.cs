﻿using MimeKit;
using System.Collections.Generic;
using System.Linq;

namespace Domer.Domain.Common.Entities;

public class Message
{
    public List<MailboxAddress> To {get;set;}
    public string Subject {get;set;}
    public string Content {get;set;}

    public Message(string?[] to, string subject, string content)
    {
        To = new List<MailboxAddress>();
        To.AddRange(to.Select(x => new MailboxAddress(x, x)));
        Subject = subject;
        Content = content;
    }
}