﻿using AdFit.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdFit.Core.Service
{
    public interface IAdvertisementService
    {
        List<Advertisement> GetAllBytes(List<Advertisement> ads);
        List<Advertisement> GetAll();
        public Advertisement AddAdvertisement(Advertisement adv);
        public Advertisement UpdateAdvertisement(int id, Advertisement adv);
        public void DeleteAdvertisementWithImage(int id);
        public  Advertisement GetById(int id);
        //public List<Advertisement> GetAllBytesByUserId(int id);
        public Advertisement GetByIdBytes(int id);


    }
}
