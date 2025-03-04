using AdFit.API.Models;
using AdFit.Core.Model;
using AdFit.Core.Service;
using AdFit.Data;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AdFit.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdvertisementController : ControllerBase
    {
        private readonly IAdvertisementService _advService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
       
        public AdvertisementController(IAdvertisementService advService,IMapper mapper,
                                      IUserService userService)
        {
            _advService = advService;
            _userService = userService;
            _mapper = mapper;
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
        public ActionResult<Advertisement> Post([FromForm] AdvertisementPostModel adv)
        {
            //נעשה את כל לוגיקת העלאת התמונה בקונטרולר ולא ברםוזיטורי 
            // בגלל שהPOSTMODEL מוגדר בפרויקט הAPI ואין לי גישה אליו ברפויזטורי
            
            if (adv.ImageFile == null)
            {
                Advertisement a= _mapper.Map<Advertisement>(adv);
                a.User = _userService.GetUserById(adv.UserId);
               
                if (a.User == null)
                {
                    return  BadRequest("user not found");
                }
               Advertisement advToAdd= _advService.AddAdvertisement(a);
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

                //   var myPath = Path.Combine(Environment.CurrentDirectory + "/images/" + adv.ImageFile.FileName);
                using (FileStream fs = new FileStream(myPath, FileMode.Create)) 
                {
                    adv.ImageFile.CopyTo(fs);
                    fs.Close();
                }
                adv.Image = adv.ImageFile.FileName;
                //adv.Image = adv.ImageFile.FileName;
                Advertisement advDto = _mapper.Map<Advertisement>(adv);
                advDto.User = _userService.GetUserById(adv.UserId);
                _advService.AddAdvertisement(advDto);
               

                return Ok(advDto);


            }
            catch (Exception ex)
            {
                Console.WriteLine("Error to add image in advertisement controller");
                return StatusCode(500,$"Interval Server Error: {ex.Message} ");
               
            }
            
            
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


        //[HttpPost("uploadImage")]
        //public ActionResult UploadImage([FromBody] Advertisement adv)
        //{

        //}
    }
}
