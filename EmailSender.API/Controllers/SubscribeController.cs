using EmailSender.UseCases.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmailSender.API.Controllers
{
    [ApiController, Route("api/[controller]/[action]")]
    public class SubscribeController : ControllerBase
    {
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> SubscribeToTag(Guid tagId,
            [FromServices] ISubscribeToTagUseCase useCase,
            CancellationToken cancellationToken)
        {
            await useCase.Execute(new(), tagId, cancellationToken);
            return Ok();
        }
    }
}
