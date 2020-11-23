using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderApplication.Domain.Context
{
    public class EmailSettings
    {
        public string MailFromAddress = "faiznavi@gmail.com";
        public bool UseSsl = true;
        public string Username = "test@gmail.com";
        public string Password = "*******";
        public string ServerName = "smtp.gmail.com";
        public int ServerPort = 587;
    }
}
