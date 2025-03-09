using AdFit.Core.Model;
using System.Text.Json.Serialization;

namespace AdFit.API.Models
{
    public class PagePostModel
    {
        //public int PageNumber { get; set; }

        [JsonIgnore]
        public List<Advertisement>? Advertisements { get; set; } = new List<Advertisement>();

        public int Capacity { get; set; } //the sum of full ad in the page
    }
}
