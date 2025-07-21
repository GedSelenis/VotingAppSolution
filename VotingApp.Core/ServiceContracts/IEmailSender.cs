using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VotingApp.Core.Domain.Entities;
using VotingApp.Core.DTO;

namespace VotingApp.Core.ServiceContracts
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
        Task SendVotingOptions(string email, string subject, List<PollOption> pollOptions, PollAnonymousVoteRequest pollAnonymousVoteRequest);
    }
}
