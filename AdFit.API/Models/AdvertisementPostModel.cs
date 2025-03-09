using AdFit.Core.Model;
using System.Text.Json.Serialization;

namespace AdFit.API.Models
{
    public class AdvertisementPostModel
    {
       
        public int UserId { get; set; }
        public IFormFile ImageFile { get; set; }
       // public int PageId { get; set; } // מוסיפים PageId מפורש
        public Esize Size { get; set; }
        public int NumOfWeeks { get; set; }  //כמות השבועות לפירסומת
       // public int NumOfAd { get; set; }  //מספר סידורי של הפירסומת
    }
}
