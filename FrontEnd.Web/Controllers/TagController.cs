using FrontEnd.Web.Models;
using FrontEnd.Web.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json.Nodes;
using System.Text;

namespace FrontEnd.Web.Controllers
{
    public class TagController : Controller
    {
        private HttpClient _httpStorageClient;
        private HttpClient _httpSubscribeClient;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _config;

        public TagController(IHttpClientFactory httpClient,
            IConfiguration config)
        {
            
            
            this._httpClientFactory = httpClient;
            _config = config;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Subscribe(string id, string name, CancellationToken cancellationToken)
        {
            _httpSubscribeClient = _httpClientFactory.CreateClient("Subscribe");
            _httpSubscribeClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await HttpContext.GetTokenAsync("access_token"));
            

            // Subscribe action
            var response = await _httpSubscribeClient.PostAsJsonAsync("subscribe/SubscribeToTag", id, cancellationToken);
            var result = response.IsSuccessStatusCode;
            var r = await response.Content.ReadAsStringAsync();
            
            ViewBag.Result = result;
            return RedirectToAction(nameof(More), new {id=id,name=name});
        }

        public async Task<IActionResult> More(string id, string name, CancellationToken cancellationToken)
        {
            var query = new Dictionary<string, string>()
            {
                ["tagId"] = id,
            };
            _httpStorageClient = _httpClientFactory.CreateClient("Storage");

            var uri = QueryHelpers.AddQueryString(_config["ServicesUrl:StorageApi:GetFactsByTagId"], query);
            var responce = await _httpStorageClient.GetAsync(uri, cancellationToken);

            if (responce.IsSuccessStatusCode)
            {
                var json = await responce.Content.ReadAsStringAsync(cancellationToken);
                var facts = JsonConvert.DeserializeObject<List<Fact>>(json);
                var vm = new TagViewModel() { Id = id, Name= name, Facts = facts};
                return View(vm);
            }

            return View(new TagViewModel());
        }
    }
}
