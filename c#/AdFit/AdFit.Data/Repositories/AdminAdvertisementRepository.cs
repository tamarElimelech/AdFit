using AdFit.Core.Model;
using AdFit.Core.Repositories;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdFit.Data.Repositories
{
    public class AdminAdvertisementRepository : IAdminAdvertisementRepository
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public AdminAdvertisementRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }

        public List<AdminAdvertisement> GetAdminAdvertisements()
        {
            return _context.AdminAdvertisements.Include(a => a.User).ToList();
        }
        public AdminAdvertisement GetAdminById(int id)
        {
            return _context.AdminAdvertisements
                   .Include(a => a.Page)
                   .Include(a => a.User)
                   .FirstOrDefault(x => x.Id == id);
        }

        public AdminAdvertisement AddAdminAdvertisement(AdminAdvertisement a)
        {
            try
            {
                _context.AdminAdvertisements.Add(a);
                _context.SaveChanges();
                return a;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding advertisement: {ex.Message}");
                return null;
            }
        }
        public AdminAdvertisement UpdateAdminAdvertisement(int id, AdminAdvertisement adv)
        {
            AdminAdvertisement newAdv = new AdminAdvertisement();
            foreach (AdminAdvertisement a in _context.AdminAdvertisements.ToList())
            {
                if (id == a.Id)
                {
                    if (adv.Image != null)
                        a.Image = adv.Image;
                    if (adv.Date != null)
                        a.Date = adv.Date;
                    if (adv.User != null)
                        a.User = adv.User;
                    if (adv.Page != null)
                        a.Page = adv.Page;
                    if (adv.Size != null)
                        a.Size = adv.Size;
                    if (adv.NumOfWeeks != null)
                        a.NumOfWeeks = adv.NumOfWeeks;
                    if (adv.NumOfAd != null)
                        a.NumOfAd = adv.NumOfAd;

                    newAdv = a;
                    break;
                }
            }
            _context.SaveChanges();
            return newAdv;
        }


        public void DeleteAdminAdvertisement(int id)
        {
            List<AdminAdvertisement> advs = _context.AdminAdvertisements.ToList();
            foreach (AdminAdvertisement a in advs)
            {
                if (a.Id == id)
                {
                    _context.AdminAdvertisements.Remove(a);
                    break;
                }

            }
            _context.SaveChanges();
        }
        public void AddAdminAdsToFillHoles(int i)
        {
            AdminAdvertisement a = _context.AdminAdvertisements.Include(a=>a.User)
                                    .FirstOrDefault(ad =>  (int)ad.Size == Math.Pow(2, i));
            
                Console.WriteLine(Math.Pow(2, i));
            if (a != null)
            {
                Advertisement ad = _mapper.Map<Advertisement>(a);
                Console.WriteLine(ad.Size);
                _context.Advertisements.Add(ad);
                _context.SaveChanges();
            }
        }

    }
}
