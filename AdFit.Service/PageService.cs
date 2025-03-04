using AdFit.Core.Model;
using AdFit.Core.Repositories;
using AdFit.Core.Service;
using Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdFit.Service
{
    public class PageService: IPageService
    {
        private readonly IPageRepository _pageRepository;
        public PageService(IPageRepository pageRepository)
        {
            _pageRepository = pageRepository;
        }
        public List<Page> GetAll()
        {
            return _pageRepository.GetPages();
        }
        public Page AddPage(Page page)
        {
            return _pageRepository.AddPage(page);
        }
        public Page UpdatePage(int id, Page page)
        {
            return _pageRepository.UpdatePage(id, page);
        }
        public void DeletePage(int id)
        {
            _pageRepository.DeletePage(id);
        }
    }
}
