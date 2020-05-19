using System;
using System.Collections.Generic;

namespace Application.Common.Interfaces
{
    public interface IEmailService
    {
        void SendMailDefault(string email, string emailBody, string subject, string emailFrom, string smtpServer, string emailPassword, bool emailUseSSL
            , int emailSmtpPort, ICollection<Tuple<string, byte[]>> attachments = null);
    }
}
