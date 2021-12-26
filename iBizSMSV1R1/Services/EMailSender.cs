
using iBizSMSV1R1.Models;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace iBizSMSV1R1.Services
{
    public class EMailSender : IEmailSender
    {
        private readonly IOptions<EmailSettingSendGrid> _emailSettingsSendGrid;
        private readonly ILogger<ViewModels.LoginModel> _logger;

        public EMailSender(IOptions<AuthMessageSenderOptions> optionsAccessor, IOptions<EmailSettingSendGrid> emailSettingsSendGrid, ILogger<ViewModels.LoginModel> logger)
        {
            Options = optionsAccessor.Value;
            _emailSettingsSendGrid = emailSettingsSendGrid;
            _logger = logger;
        }

        public AuthMessageSenderOptions Options { get; } //set only via Secret Manager

        public Task SendEmailAsync(string email, string subject, string message)
        {
            //return Execute(Options.SendGridKey, subject, message, email);
            string sender = _emailSettingsSendGrid.Value.Sender;
            string sendername = _emailSettingsSendGrid.Value.SenderName;
            return Execute(_emailSettingsSendGrid.Value.Password, subject, message, email, sender, sendername);
        }

        public Task Execute(string apiKey, string subject, string message, string email, string sender, string sendername)
        {
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress(sender, sendername),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };
            msg.AddTo(new EmailAddress(email));

            // Disable click tracking.
            // See https://sendgrid.com/docs/User_Guide/Settings/tracking.html
            msg.SetClickTracking(false, false);

            return client.SendEmailAsync(msg);
        }

        //public class CustomEmailConfirmationTokenProvider<TUser>
        //                               : DataProtectorTokenProvider<TUser> where TUser : class
        //{
        //    public CustomEmailConfirmationTokenProvider(IDataProtectionProvider dataProtectionProvider,
        //        IOptions<EmailConfirmationTokenProviderOptions> options)
        //                                                        : base(dataProtectionProvider, options)
        //    {

        //    }
        //}
        public class EmailConfirmationTokenProviderOptions : DataProtectionTokenProviderOptions
        {
            public EmailConfirmationTokenProviderOptions()
            {
                Name = "EmailDataProtectorTokenProvider";
                TokenLifespan = TimeSpan.FromHours(4);
            }
        }
               
    }
}
