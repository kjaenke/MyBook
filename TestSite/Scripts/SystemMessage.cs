using System;
using TestSite.Infrastructure;
using TestSite.Models;
using TestSite.Persistence;

namespace TestSite.Scripts
{
    public class SystemMessage
    {
        public void NewAction(string subject, string message)
        {
            UnitOfWork unit = new UnitOfWork(new PlutoContext());
            Mail mail = new Mail
            {
                Subject = subject,
                CreateId = "SYSTEM",
                Delete = "false",
                Date = DateTime.Now.ToString(),
                Message = message,
                Type = "system"
            };

            unit.Mails.Add(mail);
            unit.Complete();
        }
    }
}