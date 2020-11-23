using OrderApplication.Domain.Abstract;
using OrderApplication.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace OrderApplication.Domain.Context
{
    public class EmailOrderProcessor : IOrderProcessor
    {

        private EmailSettings emailSettings;

        public EmailOrderProcessor(EmailSettings settings)
        {
            emailSettings = settings;
        }
        public string Fstatus = "";
        public void ProcessOrder(Cart cart, ShippingDetails shippingInfo, string UsrMsg)
        {
            string msg = "";
            using (var smtpClient = new SmtpClient())
            {
                smtpClient.EnableSsl = emailSettings.UseSsl;
                smtpClient.Host = emailSettings.ServerName;
                smtpClient.Port = emailSettings.ServerPort;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials
                    = new NetworkCredential(emailSettings.Username,
                        emailSettings.Password);

                StringBuilder body = new StringBuilder()
                    .AppendLine("Thank you for becoming a valuable member:")
                    .AppendLine("---")
                    .AppendLine(UsrMsg)
                    .AppendLine("---")
                    .AppendLine("Your Details as:")
                    .AppendLine(shippingInfo.Name)
                    .AppendLine(shippingInfo.Line1)
                    .AppendLine(shippingInfo.Line2 ?? "")
                    .AppendLine(shippingInfo.City)
                    .AppendLine(shippingInfo.State ?? "")
                    .AppendLine(shippingInfo.Country)
                    .AppendLine(shippingInfo.Zip)
                    .AppendLine("---");
                MailMessage mailMessage = new MailMessage(
                    emailSettings.MailFromAddress,
                    shippingInfo.Email,
                    UsrMsg,
                    body.ToString());

                smtpClient.Send(mailMessage);
                Fstatus = "Successfull";
                
            }
        }
    }
}
