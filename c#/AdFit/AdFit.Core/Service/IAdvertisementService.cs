using AdFit.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdFit.Core.Service
{
    public interface IAdvertisementService
    {
        List<Advertisement> GetAll();
        public Advertisement AddAdvertisement(Advertisement adv);
        public Advertisement UpdateAdvertisement(int id, Advertisement adv);
        public void DeleteAdvertisementWithImage(int id);
        public  Advertisement GetById(int id);
        public List<Advertisement> GetAllByUserId(int id);


    }
}
