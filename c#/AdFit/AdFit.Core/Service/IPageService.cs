using AdFit.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdFit.Core.Service
{
    public interface IPageService
    {
        List<Page> GetAll();
        public Page AddPage(Page page);
        public Page UpdatePage(int id, Page page);
        public void DeletePage(int id);
        Page GetById(int id);
        public Esize updateAdToDuble(Advertisement ad);
        public List<Page> GetAllConvert();





    }
}
