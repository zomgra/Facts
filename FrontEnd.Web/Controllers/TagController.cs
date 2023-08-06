using FrontEnd.Web.Models;
using FrontEnd.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;

namespace FrontEnd.Web.Controllers
{
    public class TagController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public TagController(IHttpClientFactory httpClient,
            IConfiguration config)
        {
            _httpClient = httpClient.CreateClient("Storage");
            _config = config;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> More(string id, string name, CancellationToken cancellationToken)
        {
            var query = new Dictionary<string, string>()
            {
                ["tagId"] = id,
            };

            var uri = QueryHelpers.AddQueryString(_config["ServicesUrl:StorageApi:GetFactsByTagId"], query);
            var responce = await _httpClient.GetAsync(uri, cancellationToken);

            if (responce.IsSuccessStatusCode)
            {
                var json = await responce.Content.ReadAsStringAsync();
                var facts = JsonConvert.DeserializeObject<List<Fact>>(json);
                var vm = new TagViewModel() { Id = id, Name= name, Facts = facts};
                return View(vm);
            }

            return View(new TagViewModel());
        }
    }
}
