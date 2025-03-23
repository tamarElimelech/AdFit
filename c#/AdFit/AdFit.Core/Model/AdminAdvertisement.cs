using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AdFit.Core.Model
{
    public class AdminAdvertisement
    {
        public int Id { get; set; }
        public User User { get; set; }
        public string Image { get; set; }
        public DateTime Date { get; set; }

        [JsonIgnore]
        public Page? Page { get; set; }

        public Esize Size { get; set; }
        public int NumOfWeeks { get; set; } 
        public int NumOfAd { get; set; } 
        
    }
}
