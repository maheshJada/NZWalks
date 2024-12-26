using Microsoft.AspNetCore.Mvc;
using NZWalks.UI.Models;
using NZWalks.UI.Models.DTO;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace NZWalks.UI.Controllers
{
    public class RegionsController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;

        public RegionsController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<RegionDTO> responce = new List<RegionDTO>();

            try
            {
                //Get all regions from api
                var client = httpClientFactory.CreateClient();
                var httpResponce = await client.GetAsync("https://localhost:7276/api/regions");
                httpResponce.EnsureSuccessStatusCode();
                responce.AddRange( await httpResponce.Content.ReadFromJsonAsync<IEnumerable<RegionDTO>>());
            }
            catch (Exception ex) 
            { 
            }
            return View(responce);
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult>Add(AddRegionViewModel model)
        {
            var client=httpClientFactory.CreateClient();
            var httprequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://localhost:7276/api/regions"),
                Content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json")
            };
           var httpRespnceMEssage= await client.SendAsync(httprequestMessage);
            httpRespnceMEssage.EnsureSuccessStatusCode();
            var respnce=await httpRespnceMEssage.Content.ReadFromJsonAsync<RegionDTO>();
            if (respnce != null)
            { 
                return RedirectToAction("Index","Regions");
            }
            return View();

        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var client = httpClientFactory.CreateClient();
            var response= await client.GetFromJsonAsync<RegionDTO>($"https://localhost:7276/api/regions{id.ToString()}");
            if(response != null)
            {
                return View(response);
            }
            return View(null);
        }

        [HttpPut]
        public async Task<IActionResult> Edit(RegionDTO request)
        {
            var client= httpClientFactory.CreateClient();
            var httprequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"https://localhost:7276/api/regions/{request.Id}"),
                Content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json")
            };
           var httpRespoceMEssage= await client.SendAsync(httprequestMessage);
            httpRespoceMEssage.EnsureSuccessStatusCode();
            var respnce = await httpRespoceMEssage.Content.ReadFromJsonAsync<RegionDTO>();
            if (respnce != null) 
            {
                return RedirectToAction("Edit", "Regions");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(RegionDTO request)
        {
            try
            {

                var client = httpClientFactory.CreateClient();
                var httpResponseMEssage = await client.DeleteAsync($"https://localhost:7276/api/regions/{request.Id}");
                httpResponseMEssage.EnsureSuccessStatusCode();
                return RedirectToAction("Index", "Regions");
            }
            catch (Exception ex)
            {

            }
            return View("Edit");


        }
    }
}


