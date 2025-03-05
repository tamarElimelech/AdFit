using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AdFit.Core.Model
{
    public class Newspaper
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime PublicationDate { get; set; }

        public User Manager { get; set; }
        [JsonIgnore]
        public List<Page> pages { get; set; }

    }
}
