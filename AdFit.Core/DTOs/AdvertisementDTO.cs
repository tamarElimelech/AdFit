using AdFit.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdFit.Core.DTOs
{
    public class AdvertisementDTO
    {
        
        public User User { get; set; }
        public string Image { get; set; }
        public DateTime Date { get; set; }
        public Page? Page { get; set; }
        public Esize Size { get; set; }
        public int NumOfWeeks { get; set; }  //כמות השבועות לפירסומת

        public int NumOfAd { get; set; }  //מספר סידורי של הפירסומת
    }
}
