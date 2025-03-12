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
    public class AdminAdvertisementService : IAdminAdvertisementService
    {
        private readonly IAdminAdvertisementRepository _advAdminRepository;
        public AdminAdvertisementService(IAdminAdvertisementRepository advAdminRepository)
        {
            _advAdminRepository = advAdminRepository;
        }
        public List<AdminAdvertisement> GetAdminAll()
        {
            return _advAdminRepository.GetAdminAdvertisements();
        }

        public AdminAdvertisement AddAdminAdvertisement(AdminAdvertisement adv)
        {
            return _advAdminRepository.AddAdminAdvertisement(adv);
        }
        public AdminAdvertisement UpdateAdminAdvertisement(int id, AdminAdvertisement adv)
        {
            return _advAdminRepository.UpdateAdminAdvertisement(id, adv);
        }

        public void DeleteAdminAdvertisement(int id)
        {
            _advAdminRepository.DeleteAdminAdvertisement(id);
        }
        public AdminAdvertisement GetAdminById(int id)
        {
            AdminAdvertisement ad = _advAdminRepository.GetAdminById(id);
            return ad;
        }
        public void AddAdminAdsToFillHoles(bool[] empties)
        {
            for (int i = 0; i < empties.Length; i++)
            {
                if (empties[i])
                {
                    _advAdminRepository.AddAdminAdsToFillHoles(i);
                }
            }
        }
    }
}
