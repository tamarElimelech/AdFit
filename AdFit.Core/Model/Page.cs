using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdFit.Core.Model
{

    public class Page
    {
      public int Id { get; set; }
      public int PageNumber { get; set; }
      public List<Advertisement> Advertisements { get; set; }

      public int Capacity { get; set; } //the sum of full ad in the page

    }
}
