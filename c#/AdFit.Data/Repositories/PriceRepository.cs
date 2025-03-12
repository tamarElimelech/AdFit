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
    public class PriceRepository:IPriceRepository
    {
        private readonly DataContext _context;

        public PriceRepository(DataContext context)
        {
            _context = context;
        }

        public List<Price> GetAll()
        {
            return _context.Prices.ToList();
        }
        public Price AddPrice(Price price)
        {
            _context.Prices.Add(price);
            _context.SaveChanges();
            return price;
        }
        public Price UpdatePrice(int id, Price price)
        {
            Price newPrice = new Price();
            foreach (Price p in _context.Prices.ToList())
            {
                if (id == p.Id)
                {
                    if (price.Size != null)
                        p.Size = price.Size;
                    if (price.AdPrice != null)
                        p.AdPrice = price.AdPrice;
                    newPrice = p;
                    break;
                }
            }
            _context.SaveChanges();
            return newPrice;

        }
        public void DeletePrice(int id)
        {
            Price p = _context.Prices.Find(id);
            _context.SaveChanges();
            _context.Prices.Remove(p);
            _context.SaveChanges();
        }

        public Price GetById(int id)
        {
            return _context.Prices.FirstOrDefault(x => x.Id == id);
        }

    }
}
