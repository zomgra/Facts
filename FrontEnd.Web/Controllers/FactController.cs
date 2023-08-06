using FrontEnd.Web.Models;
using FrontEnd.Web.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace FrontEnd.Web.Controllers
{
    public class FactController : Controller
    {
        private readonly HttpClient _storageClient;
        private readonly IConfiguration _configuration;

        public FactController(IHttpClientFactory httpClientFactory,
            IConfiguration configuration)
        {
            _storageClient = httpClientFactory.CreateClient("Storage");
            _configuration = configuration;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.AccessToken = await HttpContext.GetTokenAsync("access_token");
            var responce = await _storageClient.GetAsync(_configuration["ServicesUrl:StorageApi:GetAllFacts"]);
            if (responce.IsSuccessStatusCode)
            {

                var json = await responce.Content.ReadAsStringAsync();
                var facts = JsonConvert.DeserializeObject<List<Fact>>(json);


                return View(new FactsViewModel
                {
                    Facts = facts,
                });
            }
            return View(new FactsViewModel());
        }
    }
}
