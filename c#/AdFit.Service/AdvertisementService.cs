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
        public AdvertisementService(IAdvertisementRepository advRepository)
        {
            _advRepository = advRepository;
        }

        public List<Advertisement> GetAll()
        {
            return _advRepository.GetAdvertisements();
        }
        public List<Advertisement> GetAllBytes()
        {
            List<Advertisement> advertisements= _advRepository.GetAdvertisements();
            foreach (Advertisement a in advertisements)
            {
                a.Image = GetImage(a.Image);
            }
            return advertisements;
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

        
        public void DeleteAdvertisement(int id)
        {
            _advRepository.DeleteAdvertisement(id);
        }
      

        public Advertisement GetByIdBytes(int id)
        {
            Advertisement ad = _advRepository.GetById(id);
            ad.Image = GetImage(ad.Image);
            return ad;
        }

         public Advertisement GetById(int id)
        {
            Advertisement ad = _advRepository.GetById(id);
            return ad;
        }

      

  
        }
    }
