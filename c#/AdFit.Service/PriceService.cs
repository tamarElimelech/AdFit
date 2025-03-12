using AdFit.Core.Model;
using AdFit.Core.Repositories;
using AdFit.Core.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdFit.Service
{
    public class PriceService : IPriceService
    {

        private readonly IPriceRepository _priceRepository;
        public PriceService(IPriceRepository priceRepository)
        {
            _priceRepository = priceRepository;
        }
        public List<Price> GetAll()
        {
            return _priceRepository.GetAll();
        }
        public Price AddPrice(Price p)
        {
            return _priceRepository.AddPrice(p);
        }
        public Price UpdatePrice(int id, Price price)
        {
            return _priceRepository.UpdatePrice(id, price);
        }
        public void DeletePrice(int id)
        {
            _priceRepository.DeletePrice(id);
        }

        public Price GetById(int id)
        {
            return _priceRepository.GetById(id);
        }
    }
}
