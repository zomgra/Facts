using Microsoft.AspNetCore.Mvc;
using Storage.UseCases.Tags.CreateTag;

namespace Storage.API.Controllers
{
    [ApiController, Route("api/[controller]/[action]")]
    public class TagController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateTagAsync(string name,
            CancellationToken cancellationToken,
            [FromServices] ICreateTagUseCase useCase)
        {
            var tag = await useCase.Excecute(name, cancellationToken);
            return Ok(tag);
        }
    }
}
