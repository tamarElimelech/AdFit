using AdFit.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdFit.Core.Repositories
{
    public interface INewspaperRepository
    {
        List<Newspaper> GetNewspapers();
        Newspaper AddNewspaper(Newspaper np);
        Newspaper UpdateNewspaper(int id, Newspaper np);
        void DeleteNewspaper(int id);
    }
}
