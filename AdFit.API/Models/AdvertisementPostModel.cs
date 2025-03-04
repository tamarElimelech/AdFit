using AdFit.Core.Model;

namespace AdFit.API.Models
{
    public class AdvertisementPostModel
    {
        //public User User { get; set; }
        public int UserId { get; set; }
        public IFormFile ImageFile { get; set; }
        public string Image { get; set; }
       // public Page? Page { get; set; }

        public Esize Size { get; set; }
        public int NumOfWeeks { get; set; }  //כמות השבועות לפירסומת

        public int NumOfAd { get; set; }  //מספר סידורי של הפירסומת
    }
}
