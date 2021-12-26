using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iBizSMSV1R1.Models
{
    public class EmailSetting
    {
        public string SMTPClient { get; set; }
        public string MailServer { get; set; }
        public int MailPort { get; set; }      
        public string Username { get; set; }
        public string Password { get; set; }
        public string SenderName { get; set; }
        public string Sender { get; set; }
    }

    public class EmailSettingSendGrid
    {       
        public string MailServer { get; set; }
        public int MailPort { get; set; }
        public bool credential { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string SenderName { get; set; }
        public string Sender { get; set; }
    }
}
