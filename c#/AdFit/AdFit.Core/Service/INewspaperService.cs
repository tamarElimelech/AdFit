using AdFit.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdFit.Core.Service
{
    public interface INewspaperService
    {
        List<Newspaper> GetAll();
        public Newspaper AddNewspaper(Newspaper np);
        public Newspaper UpdateNewspaper(int id, Newspaper np);
        public void DeleteNewspaper(int id);
    }

}
