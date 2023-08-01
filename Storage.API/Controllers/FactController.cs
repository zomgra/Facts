using Microsoft.AspNetCore.Mvc;
using Storage.UseCases.Facts.CreateFact;
using Storage.UseCases.Facts.GetFacts;

namespace Storage.API.Controllers
{
    [ApiController, Route("api/[controller]/[action]")]
    public class FactController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateFactAsync(string content,
            Guid[] ids,
            CancellationToken cancellationToken,
            [FromServices] ICreateFactUseCase useCase)
        {
            var vm = await useCase.Excecute(content, cancellationToken, ids);
            return Ok(vm);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllFactsAsync(int page,
            CancellationToken cancellation,
            [FromServices] IGetFactsUseCase useCase
            )
        {
            var facts = await useCase.Execute(page, cancellation);
            return Ok(facts);
        }
    }
}
