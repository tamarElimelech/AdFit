using AdFit.Core.Model;
using AdFit.Core.Service;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AdFit.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdvertisementController : ControllerBase
    {
        private readonly IAdvertisementService _advService;
        public AdvertisementController(IAdvertisementService advService)
        {
            _advService = advService;
        }

        // GET: api/<AdvertisementController>
        [HttpGet]
        public List<Advertisement> Get()
        {
            return _advService.GetAll();
        }

        // GET api/<AdvertisementController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<AdvertisementController>
        [HttpPost]
        public Advertisement Post([FromBody] Advertisement adv)
        {
            return _advService.AddAdvertisement(adv);
        }

        // PUT api/<AdvertisementController>/5
        [HttpPut("{id}")]
        public Advertisement Put(int id, [FromBody] Advertisement adv)
        {
            return _advService.UpdateAdvertisement(id, adv);
        }

        // DELETE api/<AdvertisementController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _advService.DeleteAdvertisement(id);
        }
    }
}
