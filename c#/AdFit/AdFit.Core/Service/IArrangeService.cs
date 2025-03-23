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
        public void PlacingAdvertisementsOnPages();


        public bool[] GetEmptySlot();

        public void FillingHolesAndSendingToCustomers();

        void SendEmail(Advertisement ad, string subject, string msg);
    }
}
