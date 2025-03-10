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
  //  [Authorize]
    public class PageController : ControllerBase
    {

        static bool flag=false;
        private readonly IPageService _pageService;
        private readonly IAdvertisementService _advService;
        private readonly IArrangeService _arrangeService;
        private readonly IMapper _mapper;

        public PageController(IPageService pageService, IAdvertisementService advService,
                              IArrangeService arrangeService,IMapper mapper)
        {
            _pageService = pageService;
            _advService = advService;
            _arrangeService = arrangeService;
            _mapper = mapper;
           
        }

        // GET: api/<PageController>
        [HttpGet]
        public List<Page> Get()
        {
            _arrangeService.PlacingAdvertisementsOnPages();
            return _pageService.GetAll();
        }

        // GET api/<PageController>/5
        [HttpGet("{id}")]
        public Page Get(int id)
        {
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

        // POST api/<PageController>
        [HttpGet("GetResponseFromCustomers")]
        public ActionResult GetResponseFromCustomers([FromQuery] string response, [FromQuery] string adId)
        {
            bool[] empties = _arrangeService.GetEmptySlot();
            Advertisement ad = _advService.GetById(int.Parse(adId));
            if (ad == null)
            {
                return NotFound();
            }
            int index = (int)Math.Log((int)ad.Size, 2);
            if (response.Equals("true") && empties[index]==true)
            {
                ad.Size = _pageService.updateAdToDuble(ad);
                _advService.UpdateAdvertisement(ad.Id, ad);
                Page p = _pageService.GetById(ad.Page.Id);
                if (p != null)
                {
                    p.Capacity = p.Capacity * 2;
                    _pageService.UpdatePage(p.Id, p);
                }
              
                Console.WriteLine("good answer");

            } 
            else if(response.Equals("true"))
                {
                    string msg = "catched";
                    string subject = "have a nice day";
                    _arrangeService.SendEmail(ad.User, subject, msg);
                }
            return Ok();
        }
    }
}
