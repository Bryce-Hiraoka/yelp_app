using System;

// define namespace
namespace yelptestapp.Models
{
    //define class FoodTypeModel
    public class FoodTypeModel
    {
        public string FoodType { get; set; } // define Foodtype to store user input
        public string Category { get; set; } // define Category to store user input
        public int Travel {get; set; } // define Travel to store user input
    }
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
