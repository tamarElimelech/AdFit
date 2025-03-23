using AdFit.Core.Model;
using AdFit.Core.Repositories;
using AdFit.Core.Service;
using Microsoft.EntityFrameworkCore;
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
        private readonly IUserRepository _userRepository;
        public AdvertisementService(IAdvertisementRepository advRepository, IUserRepository userRepository)
        {
            _advRepository = advRepository;
            _userRepository = userRepository;
        }

        public List<Advertisement> GetAll()
        {
            return _advRepository.GetAdvertisements();
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

        public Advertisement AddAdvertisement(Advertisement adv)
        {
            return _advRepository.AddAdvertisement(adv);
        }

        
        public Advertisement UpdateAdvertisement(int id, Advertisement adv)
        {
            return _advRepository.UpdateAdvertisement(id, adv);
        }

        public void DeleteAdvertisementWithImage(int id)
        {
            
            Advertisement ad = GetById(id);
            if (ad == null)
            {
                Console.WriteLine("Ad not found");
                return;
            }
            string imageName = ad.Image;
            bool isImageUsedByOtherAds = _advRepository.GetAdvertisements()
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
            _advRepository.DeleteAdvertisement(id);

        }




        public Advertisement GetById(int id)
        {
            Advertisement ad = _advRepository.GetById(id);
            return ad;
        }


        public List<Advertisement> GetAllByUserId(int id)
        {
            User u = _userRepository.GetUserById(id);
            List<Advertisement> list = u.Advertisements;
            u.Advertisements = GetAllBytes(list);
            return u.Advertisements;
        }
        public List<Advertisement> GetAllBytes(List<Advertisement> ads)
        {
            foreach (Advertisement a in ads)
            {
                a.Image = GetImage(a.Image);
            }
            return ads;
        }

    }


    }
