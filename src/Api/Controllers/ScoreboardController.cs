using Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ScoreboardController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public ScoreboardStatusModel GetStatus()
        {
            return new ScoreboardStatusModel
            {

            };
        }
    }
}
