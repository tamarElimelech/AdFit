using AdFit.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdFit.Core.Service
{
    public interface IPriceService
    {
        List<Price> GetAll();
        public Price AddPrice(Price price);
        public Price UpdatePrice(int id, Price price);
        public void DeletePrice(int id);
        Price GetById(int id);
    }
}
