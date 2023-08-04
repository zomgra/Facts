using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EmailSender.API.Controllers.Base
{
    [ApiController, Route("api/[controller]/[action]")]
    public class BaseApiController : ControllerBase
    {
        protected string UserEmail => User.FindFirst(ClaimTypes.Email)?.Value;
        protected Guid UserId => Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
    }
}
