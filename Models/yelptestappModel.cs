namespace yelptestapp.Models
{
    public class Business
    {
        // Properties
        public string Id { get; set; }          // Unique identifier for the business
        public string name { get; set; }        // Name of the restaurant
        public double rating { get; set; }      // Rating of the restaurant
        public Location location { get; set; }  // Address/location of the restaurant
        // Add more properties as needed

        // Constructor 
        public Business()
        {
            location = new Location(); // Initialize the Location property
        }

        // Methods
        public string GetFormattedAddress()
        {
            return $"{location.address1}, {location.city}";
        }

        // Add more methods as needed
    }

    public class Location
    {
        public string address1 { get; set; }  // Street address
        public string city { get; set; }      // City
        public string State { get; set; }     // State
        public string ZipCode { get; set; }   // Zip code
    }
}
