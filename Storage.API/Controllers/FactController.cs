using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Storage.API.Publisher;
using Storage.Core.ViewModels;
using Storage.UseCases.Facts.CreateFact;
using Storage.UseCases.Facts.GetFacts;
using Storage.UseCases.Facts.GetListByTagId;

namespace Storage.API.Controllers
{
    [ApiController, Route("api/[controller]/[action]")]
    public class FactController : ControllerBase
    {
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateFactAsync(string content,
            Guid[] ids,
            CancellationToken cancellationToken,
            [FromServices] ICreateFactUseCase useCase,
            [FromServices] INewFactPublisher publisher)
        {
            var vm = await useCase.Excecute(content, cancellationToken, ids);
            await publisher.Publish(vm);
            return Ok(vm);
        }
        [HttpGet]
        [ProducesResponseType(statusCode: 200,Type = typeof(IEnumerable<FactViewModel>))]
        public async Task<IActionResult> GetAllFactsAsync(int page,
            CancellationToken cancellation,
            [FromServices] IGetFactsUseCase useCase)
        {
            var facts = await useCase.Execute(page, cancellation);
            return Ok(facts);
        }
        [HttpGet]
        [ProducesResponseType(statusCode: 200, Type = typeof(IEnumerable<FactViewModel>))]
        [ProducesResponseType(statusCode: 401)]
        [ProducesResponseType(statusCode: 400)]
        public async Task<IActionResult> GetFactsByTagIdAsync(Guid tagId,
            [FromServices] IGetListByTagIdUseCase useCase,
            CancellationToken cancellationToken)
        {
            var facts = await useCase.Execute(tagId, cancellationToken); 
            return Ok(facts);
        }
    }
}
