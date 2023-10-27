using System;

namespace yelptestapp.Models
{
    public class FoodTypeModel
    {
        public string FoodType { get; set; }
        public string Category { get; set; }
        public int Travel {get; set; }
    }
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
