using AdFit.Core.Model;
using System.Text.Json.Serialization;

namespace AdFit.API.Models
{
    public class PagePostModel
    {
 

        [JsonIgnore]
        public List<Advertisement>? Advertisements { get; set; } = new List<Advertisement>();

        public int Capacity { get; set; }
    }
}
