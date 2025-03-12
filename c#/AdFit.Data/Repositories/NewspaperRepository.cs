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
    public class NewspaperRepository : INewspaperRepository
    {

        private readonly DataContext _context;
        public NewspaperRepository(DataContext context)
        {
            _context = context;
        }

        public List<Newspaper> GetNewspapers()
        {
            return _context.Newspapers.ToList();
               
        }
        public Newspaper AddNewspaper(Newspaper np)
        {
            _context.Newspapers.Add(np);
            _context.SaveChanges();
            return np;
        }
        public Newspaper UpdateNewspaper(int id, Newspaper np)
        {
            Newspaper newNewspaper = new Newspaper();
            foreach (Newspaper n in _context.Newspapers.ToList())
            {
                if (id == n.Id)
                {
                    if (np.PublicationDate != null)
                        n.PublicationDate = np.PublicationDate;
                    if (np.Manager != null)
                        n.Manager = np.Manager;
                    if(np.Name != null)
                        n.Name = np.Name;

                    newNewspaper = n;
                    break;
                }
            }
            _context.SaveChanges();
            return newNewspaper;

        }
        public void DeleteNewspaper(int id)
        {
            List<Newspaper> newspapers = _context.Newspapers.ToList();
            foreach (Newspaper np in newspapers)
            {
                if (np.Id == id)
                {
                    _context.Newspapers.Remove(np);
                    break;
                }

            }
            _context.SaveChanges();
        }
    }
}
