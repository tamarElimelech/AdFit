using System.Text.Json.Serialization;

namespace AdFit.Core.Model
{
    public enum Esize
    {
        EIGHTH = 1,
        QUARTER = 2,
        HALF = 4,
        FULL = 8
    }
    public class Advertisement
    {
        public int Id { get; set; }
        [JsonIgnore]
        public User User { get; set; }
        public string Image { get; set; }
        public DateTime Date { get; set; }

        [JsonIgnore]
        public Page? Page { get; set; }

        public Esize Size { get; set; }
        public int NumOfWeeks { get; set; }
        public int NumOfAd { get; set; }
        public DateTime? EmailSentTime { get; set; }
    }
}
