using AdFit.Core.Model;
using AdFit.Core.Service;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AdFit.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PageController : ControllerBase
    {


        private readonly IPageService _pageService;
        public PageController(IPageService pageService)
        {
            _pageService = pageService;
        }

        // GET: api/<PageController>
        [HttpGet]
        public List<Page> Get()
        {
            return _pageService.GetAll();
        }

        // GET api/<PageController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<PageController>
        [HttpPost]
        public Page Post([FromBody] Page page)
        {
            return _pageService.AddPage(page);
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
    }
}
