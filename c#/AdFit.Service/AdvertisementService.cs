﻿using AdFit.Core.Model;
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
        public List<Advertisement> GetAllBytes(List<Advertisement> ads)
        {
            foreach (Advertisement a in ads)
            {
                a.Image = GetImage(a.Image);
            }
            return ads;
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
            if (!isImageUsedByOtherAds) //אין לי עוד עם אותו שם
            {
                // שלב 2: מחיקת התמונה מהתקיה
                if (!string.IsNullOrEmpty(ad.Image))
                {
                    try
                    {
                        var imagesPath = Path.Combine(Environment.CurrentDirectory, "images");
                        var imagePath = Path.Combine(imagesPath, ad.Image);

                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath); // מחיקת הקובץ מהתקיה
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
