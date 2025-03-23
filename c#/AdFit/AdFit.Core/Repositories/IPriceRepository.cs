using AdFit.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdFit.Core.Repositories
{
    public interface IPriceRepository
    {
        public List<Price> GetAll();
        public Price GetById(int id);

        Price AddPrice(Price price);
        Price UpdatePrice(int id, Price price);
        void DeletePrice(int id);
    }
}
