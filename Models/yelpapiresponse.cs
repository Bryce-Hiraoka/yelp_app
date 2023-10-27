using System.Collections.Generic;

namespace YelpApiResponse
{
    public class YelpApiResponseModel
    {
        public List<Business> businesses { get; set; }
    }

    public class Business
    {
        public string id { get; set; } // Unique identifier for the business
        public string name { get; set; } // Name of the restaurant
        public double rating { get; set; } // Rating of the restaurant
        public Location location { get; set; } // Address/location of the restaurant
        // Add more properties as needed

        public class Location
        {
            public string address1 { get; set; } // Street address
            public string city { get; set; } // City
            public string state { get; set; } // State
            public string zip_code { get; set; } // Zip code
        }
    }
}