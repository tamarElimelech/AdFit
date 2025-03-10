using AdFit.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdFit.Core.Service
{
    public interface IArrangeService
    {
        //public Advertisement[] AdvertisementsBySize(List<User> users);
        //public bool[] GetEmptySlot(Advertisement[] advertisements);
        
        public void PlacingAdvertisementsOnPages();

        public void SendEmail(User u,string subject, string msg);

        public bool[] GetEmptySlot();

        public void FillingHolesAndSendingToCustomers();


    }
}
