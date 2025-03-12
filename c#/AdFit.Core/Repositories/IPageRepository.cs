using AdFit.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdFit.Core.Repositories
{
    public interface IPageRepository
    {
        List<Page> GetPages();
        Page AddPage(Page page);
        Page UpdatePage(int id, Page page);
        void DeletePage(int id);

        Page GetById(int id);
    }
}
