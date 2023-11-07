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

// defines the controller with namespace yelptestapp
namespace yelptestapp.Controllers
{
    // defines a controller class
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        // handles the logger for the web app
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index() // Function that opends the start page
        {
            return View(); // Function that opens Views page
        }

       [HttpPost] // This attribute specifies that this action responds to HTTP POST requests
        public IActionResult Index(FoodTypeModel model) // Function that runs the 
        {
            if (ModelState.IsValid) // checks if the inputs are valid
            {
                string foodType = model.FoodType; // assign foodType the input given by user
                string category = model.Category; // assign category the input given by user
                int travel = model.Travel; // assign travel the input given by user

                // open Success page and pass foodType, category, and travel as query params
                return RedirectToAction("Success", new { foodType = foodType, category = category, travel = travel}); 

            }
            
            // if inputs are not valid open success without params
            return RedirectToAction("Success");
        }

        // api key for yelp
        private string apiKey = "6sP7WugTooLeKqacAfWN0NZs9QAS4T7xILAsDWLYZ-MpU2yD8F4mcDaAag1NpmqPNBV8WdBz-zQ7VKzYOWgPXHJS1iRfjNiCg7w2O_1-3O1jwO_36hTRn4yvwy04ZXYx"; 
    
        public async Task<ActionResult> Success() // function to open success page
        {
            string endpoint = "https://api.yelp.com/v3/businesses/search"; // define api endpoint 
            string location = Request.Query["foodType"]; // get location input from url
            string category = Request.Query["category"]; // get category input from url
            int travel; // define int travel
            int.TryParse(Request.Query["travel"], out travel); // convert query string from url to an int           
            int distance = travel * 1609; // convert miles into meters for api
            string term = "restaurant"; // makes sure api only calls for restaurants

            // define http client
            using (HttpClient client = new HttpClient())
            {
                // send request with proper headers
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

                // define string parameters which hold parameters we want to call
                string parameters = $"location={location}&term={term}&categories={category}&radius={distance}";
                // define request uri
                string requestUri = $"{endpoint}?{parameters}";

                // send http request and store reponse
                HttpResponseMessage response = await client.GetAsync(requestUri);

                if (response.IsSuccessStatusCode) // test if reponse was successful
                {
                    string json = await response.Content.ReadAsStringAsync(); // define string json that stores reponse

                    // convert json to c# iteratble object
                    var apiResponse = JsonConvert.DeserializeObject<YelpApiResponse.YelpApiResponseModel>(json);

                    // check if reponse is empty
                    if (apiResponse != null && apiResponse.businesses != null)
                    {
                        // if so load success page and pass api response
                        return View(apiResponse.businesses);
                    }
                }

                return View("Error"); // if not load error page
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
