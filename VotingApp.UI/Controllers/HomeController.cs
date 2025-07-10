using Microsoft.AspNetCore.Mvc;
using VotingApp.Core.DTO;
using VotingApp.Core.ServiceContracts;

namespace VotingApp.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPollService _pollService;

        public HomeController(IPollService pollService)
        {
            _pollService = pollService;
        }

        [Route("/")]
        public async Task<IActionResult> Index()
        {
            List<PollResponse> polls = await _pollService.GetAllPolls();
            return View(polls);
        }

        [Route("Details/{pollId}")]
        public async Task<IActionResult> PollDetails( [FromQueryAttribute] Guid pollId)
        {
            if (pollId == Guid.Empty)
            {
                return NotFound("Poll ID cannot be empty.");
            }
            PollResponse poll = await _pollService.GetPoll(pollId);
            if (poll == null)
            {
                return NotFound($"Poll with ID {pollId} not found.");
            }
            return View(poll);
        }

        // TODO: Add poll
    }
}
