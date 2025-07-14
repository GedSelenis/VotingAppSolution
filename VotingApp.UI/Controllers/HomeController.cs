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
        private readonly ICommentService _commentService;
        private readonly UserManager<IdentityUser> _userManager;

        public HomeController(IPollService pollService, UserManager<IdentityUser> userManager, ICommentService commentService)
        {
            _pollService = pollService;
            _userManager = userManager;
            _commentService = commentService;
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

        [Route("AddComment/{pollId}")]
        public async Task<IActionResult> AddComment(Guid pollId)
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
            CommentAddRequest commentAddRequest = new CommentAddRequest
            {
                PollId = pollId
            };
            return View(commentAddRequest);
        }

        [Route("AddComment/{pollId}")]
        [HttpPost]
        public async Task<IActionResult> AddComment(CommentAddRequest commentAddRequest)
        {
            if (commentAddRequest == null)
            {
                return BadRequest("Comment data cannot be null.");
            }

            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user == null)
            {
                return Unauthorized("User is not authenticated.");
            }
            commentAddRequest.CreatedBy = Guid.Parse(user.Id);

            await _commentService.AddComment(commentAddRequest, user.UserName ?? "");

            return RedirectToAction("PollDetails", new { pollId = commentAddRequest.PollId });
        }

        [Route("AddVote/{pollId}")]
        [HttpPost]
        public async Task<IActionResult> AddVote(Guid pollId, PollAddVoteRequest pollAddVoteRequest)
        {
            if (pollId == Guid.Empty || pollAddVoteRequest == null)
            {
                return BadRequest("Poll ID and vote data cannot be empty.");
            }
            pollAddVoteRequest.PollId = pollId;

            pollAddVoteRequest.UserName = _userManager.GetUserName(HttpContext.User) ?? string.Empty;

            PollResponse updatedPoll = await _pollService.AddVote(pollAddVoteRequest);

            return RedirectToAction("PollDetails", new { pollId = updatedPoll.Id });
        }
    }
}
