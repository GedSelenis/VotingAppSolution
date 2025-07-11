using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VotingApp.Core.Domain.Entities;
using VotingApp.Core.DTO;
using VotingApp.Core.ServiceContracts;

namespace VotingApp.UI.Controllers
{
    [Route("Home")]
    public class HomeController : Controller
    {
        private readonly IPollService _pollService;
        private readonly UserManager<IdentityUser> _userManager;

        public HomeController(IPollService pollService, UserManager<IdentityUser> userManager)
        {
            _pollService = pollService;
            _userManager = userManager;
        }

        [Route("/")]
        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            List<PollResponse> polls = await _pollService.GetAllPolls();
            return View(polls);
        }

        [Route("PollDetails/{pollId}")]
        public async Task<IActionResult> PollDetails(Guid pollId)
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

        [Route("AddPoll")]
        public IActionResult AddPoll()
        {
            return View();
        }

        [Route("AddPoll")]
        [HttpPost]
        public async Task<IActionResult> AddPoll(PollAddRequest pollAddRequest)
        {
            if (pollAddRequest == null)
            {
                return BadRequest("Poll data cannot be null.");
            }
            if (pollAddRequest.optionsText == null || !pollAddRequest.optionsText.Any())
            {
                ModelState.AddModelError("optionsText", "At least one option is required.");
            }
            List<PollOption> options = new List<PollOption>();
            foreach (string optionText in pollAddRequest.optionsText.Split(','))
            {
                PollOption option = new PollOption
                {
                    Id = Guid.NewGuid(),
                    OptionText = optionText.Trim()
                };
                options.Add(option);
            }
            pollAddRequest.CreatedBy = _userManager.GetUserName(HttpContext.User) ?? string.Empty;
            pollAddRequest.Options = options;

            await _pollService.AddPoll(pollAddRequest);

            return RedirectToAction("Index");
        }
    }
}
