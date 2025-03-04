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
    public class AdvertisementService:IAdvertisementService
    {
        private readonly IAdvertisementRepository _advRepository;
        public AdvertisementService(IAdvertisementRepository advRepository)
        {
            _advRepository = advRepository;
        }
        public List<Advertisement> GetAll()
        {
            return _advRepository.GetAdvertisements();
        }
        public Advertisement AddAdvertisement(Advertisement adv)
        {
            return _advRepository.AddAdvertisement(adv);
        }
        public Advertisement UpdateAdvertisement(int id, Advertisement adv)
        {
            return _advRepository.UpdateAdvertisement(id, adv);
        }
        public void DeleteAdvertisement(int id)
        {
            _advRepository.DeleteAdvertisement(id);
        }
    }
}
