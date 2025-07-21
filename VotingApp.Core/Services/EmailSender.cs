using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using VotingApp.Core.Domain.Entities;
using VotingApp.Core.DTO;
using VotingApp.Core.ServiceContracts;

namespace VotingApp.Core.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;

        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            string mail = "TestApplication5555@gmail.com";
            string password = _configuration["Email:Password"];

            var smtpClient = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential(mail, password),
                EnableSsl = true,
            };

            await smtpClient.SendMailAsync(
                new MailMessage(from: mail, to: email,subject,message));
        }

        public async Task SendVotingOptions(string email, string subject, List<PollOption> pollOptions, PollAnonymousVoteRequest pollAnonymousVoteRequest)
        {
            string body = "Here is vote options for you:\n\n\n";
            foreach (var option in pollOptions)
            {
                body += $"{option.OptionText}\n";
                body += $"Vote link: http://localhost:5195/AddVoteAnonymous/{pollAnonymousVoteRequest.PollId}/{option.Id}/{pollAnonymousVoteRequest.UserEmail}" + "\n\n";
            }

            await SendEmailAsync(email, subject, body);
        }
    }
}
