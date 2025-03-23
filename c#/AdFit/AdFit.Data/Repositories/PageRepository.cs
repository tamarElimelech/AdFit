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
    public class PageRepository: IPageRepository
    {

        private readonly DataContext _context;
        public PageRepository(DataContext context)
        {
            _context = context;
        }

        public List<Page> GetPages()
        {
            return _context.Pages.Include(p=>p.Advertisements).ToList();
        }
        public Page AddPage(Page p)
        {
            _context.Pages.Add(p);
            _context.SaveChanges();
            return p;
        }
        public Page UpdatePage(int id, Page page)
        {
            Page newPage=new Page();
            foreach (Page p in _context.Pages.ToList())
            {
                if (id == p.Id)
                   {
                    if (page.Advertisements != null)
                        p.Advertisements = page.Advertisements;
                    if (page.PageNumber != null)
                        p.PageNumber = page.PageNumber;
                    newPage = p;
                    break;
                    }
            }
            _context.SaveChanges();
            return newPage;

        }
        public void DeletePage(int id)
        {
            Page p = _context.Pages.Find(id);
            foreach (var ad in p.Advertisements)
            {
                ad.Page = null; 
            }
            _context.SaveChanges();
            _context.Pages.Remove(p);
            _context.SaveChanges();
        }

        public Page GetById(int id)
        {
            return _context.Pages.Include(p=>p.Advertisements).FirstOrDefault(x => x.Id == id);
        }

    }
}
