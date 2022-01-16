using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("")]
    public class StatusController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public void GetHealth()
        {
        }
    }
}
