using AdFit.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdFit.Core.Service
{
    public interface IAdminAdvertisementService
    {
        List<AdminAdvertisement> GetAdminAll();
        public AdminAdvertisement AddAdminAdvertisement(AdminAdvertisement adv);
        public AdminAdvertisement UpdateAdminAdvertisement(int id, AdminAdvertisement adv);
        public void DeleteAdmiAdvertisementWithImage(int id);
        public AdminAdvertisement GetAdminById(int id);
        public void AddAdminAdsToFillHoles(bool[] empties);

    }
}
