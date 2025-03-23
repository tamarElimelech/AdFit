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
            List<AdminAdvertisement> list = _advAdminRepository.GetAdminAdvertisements();
            foreach (AdminAdvertisement ad in list)
            {
                ad.Image=GetImage(ad.Image);
            }
            return list;
        }

        public AdminAdvertisement AddAdminAdvertisement(AdminAdvertisement adv)
        {
            return _advAdminRepository.AddAdminAdvertisement(adv);
        }
        public AdminAdvertisement UpdateAdminAdvertisement(int id, AdminAdvertisement adv)
        {
            return _advAdminRepository.UpdateAdminAdvertisement(id, adv);
        }

        public void DeleteAdmiAdvertisementWithImage(int id)
        {

            AdminAdvertisement ad = GetAdminById(id);
            if (ad == null)
            {
                Console.WriteLine("Ad not found");
                return;
            }
            string imageName = ad.Image;
            bool isImageUsedByOtherAds = _advAdminRepository.GetAdminAdvertisements()
                                                       .Any(a => a.Image == imageName && a.Id != id);
            if (!isImageUsedByOtherAds) 
            {
               
                if (!string.IsNullOrEmpty(ad.Image))
                {
                    try
                    {
                        var imagesPath = Path.Combine(Environment.CurrentDirectory, "images");
                        var imagePath = Path.Combine(imagesPath, ad.Image);

                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath); 
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        return;
                    }
                }
            }
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

        private string GetImage(string ImageUrl)
        {
            if (ImageUrl != null)
            {
                var path = Path.Combine(Environment.CurrentDirectory, "images/", ImageUrl);
                byte[] bytes = System.IO.File.ReadAllBytes(path);
                string imageBase64 = Convert.ToBase64String(bytes);
                string image = string.Format("data:image/jpeg;base64,{0}", imageBase64);
                return image;
            }
            else
            {
                return null;
            }
        }

    }
}
