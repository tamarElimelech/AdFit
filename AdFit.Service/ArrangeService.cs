using AdFit.Core.Model;
using AdFit.Core.Repositories;
using AdFit.Core.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AdFit.Service
{
    public class ArrangeService : IArrangeService
    {
        private readonly IUserService _userService;
        private readonly IPageService _pageService;
        private readonly IAdvertisementService _advService;

        Random random = new Random();

        public ArrangeService(IUserService userService, IPageService pageService,IAdvertisementService advertisementService)
        {
           _userService = userService;
           _pageService = pageService;
            _advService = advertisementService;
        }
        public Advertisement[] AdvertisementsBySize(List<User> users)
        {//return array with all the adversiments for this week order by size from FULL to EIGHTH
            List<Advertisement>[] advertisementsBySize = new List<Advertisement>[4]{
              new List<Advertisement>(),
              new List<Advertisement>(),
              new List<Advertisement>(),
              new List<Advertisement>()
             };
            int cntAd = 0;
            foreach (var user in users)
            {
                foreach (var ad in user.Advertisements.ToList())
                {
                    if (ad.NumOfWeeks > 0)
                    {
                        ad.NumOfWeeks--; //remove this week
                        _advService.UpdateAdvertisement(ad.Id,ad);
                        cntAd++; //count the advertisment
                        switch ((int)ad.Size)
                        {
                            case 8:
                                advertisementsBySize[0].Add(ad);
                                break;
                            case 4:
                                advertisementsBySize[1].Add(ad);
                                break;
                            case 2:
                                advertisementsBySize[2].Add(ad);
                                break;
                            case 1:
                                advertisementsBySize[3].Add(ad);
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        _advService.DeleteAdvertisement(ad.Id);
                        user.Advertisements.Remove(ad);
                    }
                }
            }

            int j = 0;
            Advertisement[] allAdvertisementsOrderBySize = new Advertisement[cntAd];

            for (int i = 0; i < 4; i++)
            {
                foreach (var advertisement in advertisementsBySize[i])
                {
                    allAdvertisementsOrderBySize[j++] = advertisement;
                }
            }

            return allAdvertisementsOrderBySize;
        }
        public int SumAllAdd(Advertisement[] advertisements)
        {
            int sumAllAd = 0;
            for (int i = 0; i < advertisements.Length; i++)
            {
                sumAllAd += (int)advertisements[i].Size;  //sum  the whole size in the newapaper
            }

            return sumAllAd;
        }       

        public bool[] GetEmptySlot(Advertisement[] advertisements)
        {
            //return array that arr[i]=true is there is a hole in this size.
            int sumAllAd, empty;
            bool[] empties = new bool[4];

            sumAllAd=SumAllAdd(advertisements);
            empty = sumAllAd % 8; //the sum of empties

            if (empty >= (int)Esize.FULL)
            {
                empties[0] = true;
                empty -= (int)Esize.FULL;
            }
            if (empty >= (int)Esize.HALF)
            {
                empties[1] = true;
                empty -= (int)Esize.HALF;
            }
            if (empty >= (int)Esize.QUARTER)
            {
                empties[2] = true;
                empty -= (int)Esize.QUARTER;
            }
            if (empty >= (int)Esize.EIGHTH)
            {
                empties[3] = true;
                empty -= (int)Esize.EIGHTH;
            }

            return empties;
        }

        public void deleteOldPages()
        {
            List<Page> pages = _pageService.GetAll();
            foreach (Page p in pages)
            {
                _pageService.DeletePage(p.Id);
            }
        }
        //לא פונקציה סגורה
        public void PlacingAdvertisementsOnPages()
        {
            deleteOldPages();
            List<User> users = _userService.GetAll();
            Advertisement[] allAdvertisements = AdvertisementsBySize(users); //get all the advertisment sorted by size
            int sumAd= SumAllAdd(allAdvertisements);
            
            int numPages = (sumAd / 8); //num pages in this newspaper
            if (sumAd % 8 != 0)
            {
                numPages++;
            }
                Page[] pages = new Page[numPages]; //array of the pages
            int randPage;
            int NumberAdsPlaced = 0;
            while (NumberAdsPlaced<allAdvertisements.Length)
            {
                do
                {
                     randPage = random.Next(0, numPages); //זה כולל המספרים או לא?
                    if (pages[randPage] == null)
                    {
                        pages[randPage] = new Page();
                        break;
                    }
                } while (pages[randPage].Capacity==8);
                if (pages[randPage].Capacity==0)
                {
                    pages[randPage] = _pageService.AddPage(pages[randPage]);
                }
                Page selectedPage = pages[randPage];

                selectedPage.Advertisements.Add(allAdvertisements[NumberAdsPlaced]);
                selectedPage.Capacity += (int)allAdvertisements[NumberAdsPlaced].Size;
                selectedPage.PageNumber=randPage;
                Page p=_pageService.UpdatePage(selectedPage.Id,selectedPage);
                NumberAdsPlaced++;
            }
            

        }
    }
}
