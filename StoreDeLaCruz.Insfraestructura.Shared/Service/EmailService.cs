using Microsoft.Extensions.Options;
using MailKit.Net.Smtp;
using StoreDeLaCruz.Core.Aplication.DTOs.Email;
using StoreDeLaCruz.Core.Aplication.Interfaces.Service;
using StoreDeLaCruz.Core.Domain.Settings;
using MailKit.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using MimeKit;

namespace StoreDeLaCruz.Insfraestructura.Shared.Service
{
    public class EmailService : IEmailService
    {
        //IOptions<> se encarga de inyectar configuraciones
        private MailSettings _mailSettings {  get; }

        public EmailService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }

        public async Task SenAsync(EmailRequestDto request)
        {
			try
			{
                MimeMessage email = new();
                email.Sender = MailboxAddress.Parse (_mailSettings.EmailFrom);
                email.To.Add(MailboxAddress.Parse(request.To)); //Esto es para a quien le quiero enviar ese correo
                email.Subject = request.Subject;
                BodyBuilder builder = new();
                builder.HtmlBody = request.Body;
                email.Body = builder.ToMessageBody();

                //Configuracion del SMTP
                using MailKit.Net.Smtp.SmtpClient smtp = new();
                smtp.ServerCertificateValidationCallback = (s, c, h, e) => true;
                smtp.Connect(_mailSettings.SmtpHost, _mailSettings.SmtpPort, SecureSocketOptions.StartTls);
                smtp.Authenticate(_mailSettings.SmtpUser, _mailSettings.SmtpPass);
                await smtp.SendAsync(email);
                smtp.Disconnect(true);
			}
			catch (Exception ex)
			{

				
			}
        }

    }
}
