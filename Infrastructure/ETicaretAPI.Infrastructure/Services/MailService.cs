using ETicaretAPI.Application.Abstractions.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Infrastructure.Services
{
    public class MailService : IMailService
    {
        readonly IConfiguration _configuration;

        public MailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task SendMailAsync(string to, string subject, string body, bool isBodyHtml = true)
        {
            await SendMailAsync(new[] { to }, subject, body, isBodyHtml);
        }

        public async Task SendMailAsync(string[] tos, string subject, string body, bool isBodyHtml = true)
        {
            MailMessage mail = new();
            mail.IsBodyHtml = isBodyHtml;
            foreach (var to in tos)
                mail.To.Add(to);
            mail.Subject = subject;
            mail.Body = body;
            mail.From = new(_configuration["Mail:Username"], "NG E-Ticaret", System.Text.Encoding.UTF8);

            SmtpClient smtp = new();
            smtp.Credentials = new NetworkCredential(_configuration["Mail:Username"], _configuration["Mail:Password"]);
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.Host = _configuration["Mail:Host"];
            await smtp.SendMailAsync(mail);
        }

        public async Task SendPasswordResetMailAsync(string to, string userId, string resetToken)
        {
            StringBuilder mail = new();
            mail.AppendLine("Salam<br>Eger yeni şifre telebiniz varsa aşağıdaki linkten şifrenizi yenileye bilersiniz.<br><strong><a target=\"_blank\" href=\"");
            mail.AppendLine(_configuration["AngularClientUrl"]);
            mail.AppendLine("/update-password/");
            mail.AppendLine(userId);
            mail.AppendLine("/");
            mail.AppendLine(resetToken);
            mail.AppendLine("\">Yeni şifre telebi uçun tıklayın...</a></strong><br><br><span style=\"font-size:12px;\">NOT : Eger ki bu teleb siz terefden gerçekleştirilmemişse zehmet olmasa bu maili ciddiye almayın.</span><br>Saygılarımızla...<br><br><br>NG - Mini|E-Ticaret");

            await SendMailAsync(to, "Password Generate", mail.ToString());
        }

        public async Task SendCompletedOrderMailAsync(string to, string orderCode, DateTime orderDate, string userName)
        {
            string mail = $"Sayın {userName} Salam<br>" +
                $"{orderDate} tarixinde vermiş olduğunuz {orderCode} kodlu sifarişiniz tamamlanmış ve kargo firmasına verilmişdir.<br>Xeyirli olsun";

            await SendMailAsync(to, $"{orderCode} sifariş nomrelı sifarişiniz tamamlandı", mail);

        }

    }
}
