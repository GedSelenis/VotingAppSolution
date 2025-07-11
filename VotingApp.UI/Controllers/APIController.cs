using Microsoft.AspNetCore.Mvc;

namespace VotingApp.UI.Controllers
{
    [Route("api")]
    public class APIController : ControllerBase
    {
        public IActionResult Index()
        {
            return Content("API is running successfully.");
        }
    }
}
