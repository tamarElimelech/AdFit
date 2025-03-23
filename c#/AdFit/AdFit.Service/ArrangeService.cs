using AdFit.Core.Model;
using AdFit.Core.Repositories;
using AdFit.Core.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
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

        public ArrangeService(IUserService userService, IPageService pageService, IAdvertisementService advertisementService)
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
                        _advService.UpdateAdvertisement(ad.Id, ad);
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
                        _advService.DeleteAdvertisementWithImage(ad.Id);
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
        public int SumAllAdd()
        {
            List<Advertisement> advertisements = _advService.GetAll();
            int sumAllAd = 0;
            foreach (Advertisement ad in advertisements)
            {
                sumAllAd += (int)ad.Size;  //sum  the whole size in the newapaper
            }
            return sumAllAd;
        }

        public bool[] GetEmptySlot()
        {
            //return array that arr[i]=true is there is a hole in this size.
            int sumAllAd, empty;
            bool[] empties = new bool[4];

            sumAllAd = SumAllAdd();
            empty = 8 - (sumAllAd % 8); //the sum of empties
            if (empty == 8)
            {
                return empties;
            }

            if (empty >= (int)Esize.FULL)
            {
                empties[3] = true;
                empty -= (int)Esize.FULL;
            }
            if (empty >= (int)Esize.HALF)

            {
                empties[2] = true;
                empty -= (int)Esize.HALF;
            }
            if (empty >= (int)Esize.QUARTER)
            {
                empties[1] = true;
                empty -= (int)Esize.QUARTER;
            }
            if (empty >= (int)Esize.EIGHTH)
            {
                empties[0] = true;
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
       
        public void PlacingAdvertisementsOnPages()
        {
            deleteOldPages();
            List<User> users = _userService.GetAll();
            Advertisement[] allAdvertisements = AdvertisementsBySize(users); //get all the advertisment sorted by size
            int sumAd = SumAllAdd();

            int numPages = (sumAd / 8); //num pages in this newspaper
            if (sumAd % 8 != 0)
            {
                numPages++;
            }
            Page[] pages = new Page[numPages]; //array of the pages
            int randPage;
            int NumberAdsPlaced = 0;
            while (NumberAdsPlaced < allAdvertisements.Length)
            {
                do
                {
                    randPage = random.Next(0, numPages); 
                    if (pages[randPage] == null)
                    {
                        pages[randPage] = new Page();
                        break;
                    }
                } while (pages[randPage].Capacity == 8);
                if (pages[randPage].Capacity == 0)
                {
                    pages[randPage] = _pageService.AddPage(pages[randPage]);
                }
                Page selectedPage = pages[randPage];

                selectedPage.Advertisements.Add(allAdvertisements[NumberAdsPlaced]);
                selectedPage.Capacity += (int)allAdvertisements[NumberAdsPlaced].Size;
                selectedPage.PageNumber = randPage;
                Page p = _pageService.UpdatePage(selectedPage.Id, selectedPage);
                NumberAdsPlaced++;
            }
        }
        public void FillingHolesAndSendingToCustomers()
        {
            List<Advertisement> listAds = _advService.GetAll();
            bool[] arrHole = GetEmptySlot();
            for (int i = 0; i < arrHole.Length; i++)
            {
                if (!arrHole[i])
                    continue;
                foreach (Advertisement ad in listAds)
                    if ((int)ad.Size == Math.Pow(2, i))
                    {
                        string sub = "Last Chance: Double Your Ad Size for Half the Price!" + ad.Size;
                        DateTime deadline = DateTime.Now.AddDays(1);
                        string formattedDeadline = deadline.ToString("MMMM dd, yyyy");
                        string msg = $@"
                        <html>
                          <head>
                            <style>
                              .btn {{
            display: inline-block;
            padding: 12px 24px;
            font-size: 16px;
            border-radius: 5px;
            text-decoration: none;
            color: white;
            cursor: pointer;
        }}
        .confirm-btn {{ background-color: green; }}
        .decline-btn {{ background-color: red; }}
    </style>
</head>
<body>
    <p>Dear {ad.User.Name} Customer,</p>

    <p>We are excited to offer you an exclusive, limited-time opportunity to enhance your advertisement!</p>

    <p><b>For a short time, you can <u>double the size</u> of your ad for just <u>50% more</u> of the original price.</b></p>

    <ul>
        <li>✔ More visibility</li>
        <li>✔ More impact</li>
        <li>✔ More customers</li>
    </ul>

<p>Would you like to take advantage of this special offer?</p>
<a href=""https://localhost:7194/api/Page/GetResponseFromCustomers?response=true&adId={ad.Id}"" class=""btn confirm-btn"">✅ Confirm Upgrade</a>
<a href=""https://localhost:7194/api/Page/GetResponseFromCustomers?response=false&adId={ad.Id}"" class=""btn decline-btn"">❌ Decline Offer</a>

    <p><b>Hurry! This offer expires on {formattedDeadline}.</b></p>

    <p>Best regards,</p>
    <p><b>AdFit</b></p>
</body>
</html>";


                        SendEmail(ad, sub, msg);

                    }

            }
        }
        public void SendEmail(Advertisement ad, string subject, string msg)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("tamar21487@gmail.com");
            mailMessage.To.Add("tamar21487@gmail.com"); //ad.user.email
            mailMessage.Subject = subject;
            mailMessage.Body = msg;
            mailMessage.IsBodyHtml = true;

            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = "smtp.gmail.com";
            smtpClient.Port = 587;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential("tamar21487@gmail.com", "ppxd zrds ouwb jtao");
            smtpClient.EnableSsl = true;

            try
            {
                smtpClient.Send(mailMessage);
                Console.WriteLine("Email Sent Successfully.");
                ad.EmailSentTime = DateTime.Now;
                _advService.UpdateAdvertisement(ad.Id,ad);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

    }
}
