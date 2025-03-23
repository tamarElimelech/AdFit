using AdFit.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdFit.Core.Repositories
{
    public interface IAdvertisementRepository
    {
        List<Advertisement> GetAdvertisements();
        Advertisement AddAdvertisement(Advertisement adv);
        Advertisement UpdateAdvertisement(int id, Advertisement adv);
        void DeleteAdvertisement(int id);

        Advertisement GetById(int id);


        //public void AddAdminAdsToFillHoles(int i);

    }
}
