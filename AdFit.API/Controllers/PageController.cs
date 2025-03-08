﻿using AdFit.API.Models;
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
    public class PageController : ControllerBase
    {


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
    }
}
