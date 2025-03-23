using AdFit.API.Models;
using AdFit.Core.Model;
using AdFit.Core.Service;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AdFit.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AdminAdvertisementController : ControllerBase
    {
        private readonly IAdminAdvertisementService _advService;
        private readonly IUserService _userService;
        private readonly IPageService _pageService;
        private readonly IMapper _mapper;

        public AdminAdvertisementController(IAdminAdvertisementService advService, IMapper mapper,
                                      IUserService userService, IPageService pageService)
        {
            _advService = advService;
            _userService = userService;
            _pageService = pageService;
            _mapper = mapper;
        }

        // GET: api/<AdvertisementController>
        [HttpGet]
        public List<AdminAdvertisement> Get()
        {
            return _advService.GetAdminAll();
        }



        // GET api/<AdvertisementController>/5
        [HttpGet("{id}")]
        public AdminAdvertisement Get(int id)
        {
            return _advService.GetAdminById(id);
        }

        // POST api/<AdvertisementController>
        [HttpPost]
        public ActionResult<AdminAdvertisement> Post([FromForm] AdminAdvertisementPostModel adv)
        {
            AdminAdvertisement a = _mapper.Map<AdminAdvertisement>(adv);
            a.User = _userService.GetUserById(adv.UserId);
            if (a.User == null)
            {
                return BadRequest("user not found");
            }
            a.Date = DateTime.Now;
            if (adv.ImageFile == null)
            {
                AdminAdvertisement advToAdd = _advService.AddAdminAdvertisement(a);
                return Ok(advToAdd);
            }
            try
            {
                var imagesPath = Path.Combine(Environment.CurrentDirectory, "images");

                if (!Directory.Exists(imagesPath))
                {
                    Directory.CreateDirectory(imagesPath);
                }

                var myPath = Path.Combine(imagesPath, adv.ImageFile.FileName);
                using (FileStream fs = new FileStream(myPath, FileMode.Create))
                {
                    adv.ImageFile.CopyTo(fs);
                    fs.Close();
                }
                a.Image = adv.ImageFile.FileName;
                AdminAdvertisement advToAdd = _advService.AddAdminAdvertisement(a);

                if (advToAdd != null)
                {
                    return Ok(advToAdd);
                }
                else
                {
                    return BadRequest("Failed to upload to DB");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error to add image in advertisement controller");
                return StatusCode(500, $"Interval Server Error: {ex.Message}");
            }
        }

        // PUT api/<AdvertisementController>/5
        [HttpPut("{id}")]
        public AdminAdvertisement Put(int id, [FromBody] AdminAdvertisement adv)
        {
            return _advService.UpdateAdminAdvertisement(id, adv);
        }

        // DELETE api/<AdvertisementController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _advService.DeleteAdmiAdvertisementWithImage(id);
        }
    }
}
