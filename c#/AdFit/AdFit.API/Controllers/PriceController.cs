using AdFit.API.Models;
using AdFit.Core.Model;
using AdFit.Core.Service;
using AdFit.Service;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AdFit.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PriceController : ControllerBase
    {

        private readonly IPriceService _priceService;
        private readonly IMapper _mapper;

        public PriceController(IPriceService priceService, IMapper mapper)
        {
            _priceService = priceService;
            _mapper = mapper;
        }


        // GET: api/<PriceController>
        [HttpGet]
        public List<Price> Get()
        {
            return _priceService.GetAll();
        }

        // GET api/<PriceController>/5
        [HttpGet("{id}")]
        public Price Get(int id)
        {
            return _priceService.GetById(id);
        }

        // POST api/<PriceController>
        [HttpPost]
        public Price Post([FromBody] PricePostModel price)
        {
            Price p= _mapper.Map<Price>(price);
            return _priceService.AddPrice(p);
        }

        // PUT api/<PriceController>/5
        [HttpPut("{id}")]
        public Price Put(int id, [FromBody] Price price)
        {
            return _priceService.UpdatePrice(id, price);
        }

        // DELETE api/<PriceController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _priceService.DeletePrice(id);
            return Ok();
        }
    }
}
