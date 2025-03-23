using AdFit.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdFit.Core.Repositories
{
    public interface IAdminAdvertisementRepository
    {

        List<AdminAdvertisement> GetAdminAdvertisements();
        AdminAdvertisement AddAdminAdvertisement(AdminAdvertisement adv);
        AdminAdvertisement UpdateAdminAdvertisement(int id, AdminAdvertisement adv);
        void DeleteAdminAdvertisement(int id);

        AdminAdvertisement GetAdminById(int id);

        public void AddAdminAdsToFillHoles(int i);
    }
}
