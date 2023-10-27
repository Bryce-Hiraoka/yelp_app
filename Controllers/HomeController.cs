using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using yelptestapp.Models;

namespace yelptestapp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

       [HttpPost] // This attribute specifies that this action responds to HTTP POST requests
        public IActionResult Index(FoodTypeModel model)
        {
            if (ModelState.IsValid)
            {
                string foodType = model.FoodType;
                string category = model.Category;
                int travel = model.Travel;

                return RedirectToAction("Success", new { foodType = foodType, category = category, travel = travel});            
            }

            return RedirectToAction("Success");
        }

        
        private string apiKey = "6sP7WugTooLeKqacAfWN0NZs9QAS4T7xILAsDWLYZ-MpU2yD8F4mcDaAag1NpmqPNBV8WdBz-zQ7VKzYOWgPXHJS1iRfjNiCg7w2O_1-3O1jwO_36hTRn4yvwy04ZXYx"; 
    

        public async Task<ActionResult> Success()
        {
            string endpoint = "https://api.yelp.com/v3/businesses/search";
            string location = Request.Query["foodType"];
            string category = Request.Query["category"];
            int travel;
            int.TryParse(Request.Query["travel"], out travel);            
            int distance = travel * 1609;
            string term = "restaurant";

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

                string parameters = $"location={location}&term={term}&categories={category}&radius={distance}";
                string requestUri = $"{endpoint}?{parameters}";

                HttpResponseMessage response = await client.GetAsync(requestUri);

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    var apiResponse = JsonConvert.DeserializeObject<YelpApiResponse.YelpApiResponseModel>(json);
                
                    if (apiResponse != null && apiResponse.businesses != null)
                    {
                        return View(apiResponse.businesses);
                    }
                }

                return View("Error"); // Create an Error view to display an error message
            }
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
