using AdFit.API.Models;
using AdFit.Core.Model;
using AdFit.Core.Service;
using AdFit.Service;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AdFit.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
       public  UserController(IUserService userService,IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        // GET: api/<UserController>
        [HttpGet]
        public IEnumerable<User> Get()
        {
            return _userService.GetAll();
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public User Get(int id)
        {
             return _userService.GetUserById(id);
        }

        // POST api/<UserController>
        [HttpPost]
        public User Post([FromBody] UserPostModel user)
        {
           return _userService.AddUser(_mapper.Map<User>(user));
        }

     

            // PUT api/<UserController>/5
            [HttpPut("{id}")]
        public User Put(int id, [FromBody] User user)
        {
            return _userService.UpdateUser(id, user);
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _userService.DeleteUser(id);
        }
    }
}
