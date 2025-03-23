using AdFit.Core.Model;

namespace AdFit.API.Models
{
    public class AdminAdvertisementPostModel
    {
        public int UserId { get; set; }
        public IFormFile ImageFile { get; set; }
        public Esize Size { get; set; }
        public int NumOfWeeks { get; set; }  
    }
}
