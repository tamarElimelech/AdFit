using AdFit.Core.Model;
using AdFit.Core.Repositories;
using AdFit.Core.Service;
using Azure;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IAdvertisementService _advertisementService;
        public PageService(IPageRepository pageRepository,IAdvertisementService advertisementService)
        {
            _pageRepository = pageRepository;
            _advertisementService = advertisementService;
        }
        public List<Page> GetAll()
        {
            List<Page> pages= _pageRepository.GetPages();
            foreach (Page p in pages)
            {
                p.Advertisements = _advertisementService.GetAllBytes(p.Advertisements);
            }
            return pages;
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

      public  Page GetById(int id)
        {
            return _pageRepository.GetById(id);
        }

        public Esize updateAdToDuble( Advertisement ad)
        {
                int sizeValue = (int)ad.Size;
                sizeValue *= 2;
                ad.Size = (Esize)sizeValue;
            return ad.Size;
    }

}
}
