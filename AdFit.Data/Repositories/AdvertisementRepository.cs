using AdFit.Core.Model;
using AdFit.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdFit.Data.Repositories
{
    public class AdvertisementRepository:IAdvertisementRepository
    {
        private readonly DataContext _context;
        public AdvertisementRepository(DataContext context)
        {
            _context = context;
        }

        public List<Advertisement> GetAdvertisements()
        {
            return _context.Advertisements.ToList();
        }
        public Advertisement AddAdvertisement(Advertisement a)
        {
            _context.Advertisements.Add(a);
            _context.SaveChanges();
            return a;
        }
        public Advertisement UpdateAdvertisement(int id, Advertisement adv)
        {
            Advertisement newAdv = new Advertisement();
            foreach (Advertisement a in _context.Advertisements.ToList())
            {
                if (id == a.Id)
                {
                    if (adv.Image != null)
                        a.Image = adv.Image;
                    if(adv.User != null)
                        a.User = adv.User;  
                    if(adv.Date!=null)
                        a.Date = adv.Date;
                    if(adv.Page!=null)
                        a.Page= adv.Page;
                    if(adv.Size!=null)
                        a.Size = adv.Size;
                    if(adv.NumOfWeeks!=null)
                        a.NumOfWeeks = adv.NumOfWeeks;
                    if(adv.NumOfAd!=null)
                        a.NumOfAd = adv.NumOfAd;

                    newAdv = a;
                    break;
                  }
               }
            _context.SaveChanges();
            return newAdv;
        }

        public void DeleteAdvertisement(int id)
        {
            List<Advertisement> advs = _context.Advertisements.ToList();
            foreach (Advertisement a in advs)
            {
                if (a.Id == id)
                {
                    _context.Advertisements.Remove(a);
                    break;
                }

            }
            _context.SaveChanges();
        }
    }
}
