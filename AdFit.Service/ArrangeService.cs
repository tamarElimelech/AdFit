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
        Random random = new Random();

        public ArrangeService(IUserService userService)
        {
            _userService = userService;
        }
        public Advertisement[] AdvertisementsBySize(List<User> users)
        {//return array with all the adversiments for this week order by size from FULL to EIGHTH
            List<Advertisement>[] advertisementsBySize = new List<Advertisement>[4];
            int cntAd = 0;
            foreach (var user in users)
            {
                foreach (var ad in user.Advertisements)
                {
                    if (ad.NumOfWeeks > 0)
                    {
                        ad.NumOfWeeks--; //remove this week
                        cntAd++; //count the advertisment
                        switch (ad.Size)
                        {
                            case Esize.FULL:
                                advertisementsBySize[0].Add(ad);
                                break;
                            case Esize.HALF:
                                advertisementsBySize[1].Add(ad);
                                break;
                            case Esize.QUARTER:
                                advertisementsBySize[2].Add(ad);
                                break;
                            case Esize.EIGHTH:
                                advertisementsBySize[3].Add(ad);
                                break;
                            default:
                                break;
                        }
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
        //לא פונקציה סגורה
        public Page[] PlacingAdvertisementsOnPages(Advertisement[] advertisements)
        {

            List<User> users = _userService.GetAll();
            Advertisement[] allAdvertisements = AdvertisementsBySize(users); //get all the advertisment sorted by size
            int sumAd= SumAllAdd(allAdvertisements);
            int numPages = (sumAd / 8) + 1; //num pages in this newspaper
            Page[] pages = new Page[numPages]; //array of the pages
            int randPage;
            int NumberAdsPlaced = 0;
            while (NumberAdsPlaced<allAdvertisements.Length)
            {
                do
                {
                     randPage = random.Next(0, numPages + 1);
                    if (pages[randPage] == null)
                    {
                        pages[randPage] = new Page();
                        break;
                    }
                } while (pages[randPage].Capacity==8);
                Page selectedPage = pages[randPage];
                selectedPage.Advertisements.Add(allAdvertisements[NumberAdsPlaced]);
                selectedPage.Capacity = (int)allAdvertisements[NumberAdsPlaced].Size;
                selectedPage.PageNumber=randPage;
                NumberAdsPlaced++;
            }
            

     return pages;

        }



    }
}
