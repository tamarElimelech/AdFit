using AdFit.API.Models;
using AdFit.Core.Model;
using AdFit.Core.Service;
using AdFit.Service;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AdFit.API.Controllers
{    
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PageController : ControllerBase
    {

       
        private readonly IPageService _pageService;
        private readonly IAdvertisementService _advService;
        private readonly IAdminAdvertisementService _adminAdvService;

        private readonly IArrangeService _arrangeService;
        private readonly IMapper _mapper;

        public PageController(IPageService pageService, IAdvertisementService advService,
                              IArrangeService arrangeService,IMapper mapper, IAdminAdvertisementService adminAdvertisementService)
        {
            _pageService = pageService;
            _advService = advService;
            _arrangeService = arrangeService;
            _mapper = mapper;
            _adminAdvService = adminAdvertisementService;
           
        }

        // GET: api/<PageController>
        [HttpGet]
        public List<Page> Get()
        {
            return _pageService.GetAllConvert();
        }

        // GET api/<PageController>/5
        [HttpGet("{id}")]
        public Page Get(int id)
        {
            _arrangeService.PlacingAdvertisementsOnPages();
          return  _pageService.GetById(id);
        }

        // POST api/<PageController>
        [HttpPost]
        public Page Post([FromBody] PagePostModel page)
        {
            return _pageService.AddPage(_mapper.Map<Page>(page));
        }

        // PUT api/<PageController>/5
        [HttpPut("{id}")]
        public Page Put(int id, [FromBody] Page page)
        {
            return _pageService.UpdatePage(id, page);
        }

        // DELETE api/<PageController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _pageService.DeletePage(id);
        }

        private static readonly object _lockObj = new object();


        [AllowAnonymous]
        // POST api/<PageController>
        [HttpGet("GetResponseFromCustomers")]
        public ActionResult GetResponseFromCustomers([FromQuery] string response, [FromQuery] string adId)
        {
            lock (_lockObj) 
            {
                bool[] empties = _arrangeService.GetEmptySlot();
                Advertisement ad = _advService.GetById(int.Parse(adId));
                if (ad == null)
                {
                    return NotFound();
                }
                DateTime sendTime = (DateTime)ad.EmailSentTime;
                DateTime deadline = sendTime.AddDays(1);
                if (DateTime.Now > deadline)
                {
                    string msg = "This offer has expired.";
                    string subject = "sorry, have a nice day";
                    _arrangeService.SendEmail(ad, subject, msg);
                    return Ok();
                }
                int index = (int)Math.Log((int)ad.Size, 2);
                if (response.Equals("true") && empties[index] == true)
                {
                    ad.Size = _pageService.updateAdToDuble(ad);
                    _advService.UpdateAdvertisement(ad.Id, ad);
                }
                else if (response.Equals("true"))
                {
                    string msg = "the offer catched on size " + ad.Size;
                    string subject = "sory, have a nice day";
                    _arrangeService.SendEmail(ad, subject, msg);
                }
                return Ok();
            }
        }

        [HttpGet("getEmpties")]
        public ActionResult<bool[]> getEmpties()
        {
            return _arrangeService.GetEmptySlot(); 

        }

        [HttpPost("sendEmailsToCustomers")]
        public ActionResult sendEmailsToCustomers()
        {
             _arrangeService.FillingHolesAndSendingToCustomers();
            return Ok();
        }


        [HttpGet("ArrangePages")]
        public ActionResult<List<Page>> ArrangePages()
        {
          
                bool hole = false;
                bool[] empties = _arrangeService.GetEmptySlot();
                foreach (bool e in empties)
                {
                if(e){
                    hole = true;
                    break;
                }
                }

                if (hole)
                {
                _adminAdvService.AddAdminAdsToFillHoles(empties);
                }
                _arrangeService.PlacingAdvertisementsOnPages(); 
            List<Page> list =_pageService.GetAllConvert();
                return Ok(list);
        }
    }
}
