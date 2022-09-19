using Castle.DynamicProxy;
using Microsoft.Extensions.DependencyInjection;
using Reconciliation.Core.Entities.Concrete;
using Reconciliation.Core.Utilities.Interceptors;
using Reconciliation.Core.Utilities.IoC;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Reconciliation.Core.Aspects.Performance
{
    public class PerformanceAspect : MethodInterception
    {
        private int _interval;
        private Stopwatch _stopwatch;

        public PerformanceAspect(int interval)
        {
            _interval = interval;
            _stopwatch = ServiceTool.ServiceProvider.GetService<Stopwatch>();
        }

        protected override void OnBefore(IInvocation invocation)
        {
            _stopwatch.Start();

        }

        protected override void OnAfter(IInvocation invocation)
        {
            if (_stopwatch.Elapsed.TotalSeconds > _interval)
            {
                string body = $"Perforance : {invocation.Method.DeclaringType.FullName}.{invocation.Method.Name} --> {_stopwatch.Elapsed.TotalSeconds}";
                SendConfirmEmail(body);
            }
            _stopwatch.Reset();
        }

        void SendConfirmEmail(string body)
        {
            string subject = "Performans Maili";

            SendMailDto sendMailDto = new SendMailDto()
            {
                Email = "miyagiz@zohomail.eu",
                Password = "Klavye12Ko",
                Port = 587,
                SMTP = "smtp.zoho.eu",
                SSL = true,
                email = "yagiz201551@gmail.com",
                subject = subject,
                body = body
            };

            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(sendMailDto.Email);
                mail.To.Add(sendMailDto.email);
                mail.Subject = sendMailDto.subject;
                mail.Body = sendMailDto.body;
                mail.IsBodyHtml = true;
                //mail.Attachments.Add();

                using (SmtpClient smtp = new SmtpClient(sendMailDto.SMTP))
                {
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(sendMailDto.Email, sendMailDto.Password);
                    smtp.EnableSsl = sendMailDto.SSL;
                    smtp.Port = sendMailDto.Port;
                    smtp.Send(mail);
                }
            }
        }

    }
}
