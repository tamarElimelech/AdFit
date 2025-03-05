using AdFit.API.Models;
using AdFit.Core.Model;
using AdFit.Core.Service;
using AdFit.Service;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AdFit.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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

        [HttpPost("signIn")]
        public ActionResult<User> SignIn([FromBody] SignInModel signInUser)
        {
            string errorMessage;
            var user = _userService.SignIn(signInUser, out errorMessage);

            if (user == null)
            {
                if (errorMessage == "User not found")
                {
                    return NotFound("User not found");
                }

                if (errorMessage == "Incorrect password")
                {
                    return Conflict("Incorrect password");
                }
            }

            return Ok(user);

        }

        [HttpPost("signUp")]
        public ActionResult<User> SignUp([FromBody] UserPostModel user)
        {
            User u=_userService.SignUp(_mapper.Map<User>(user));
            if(u == null)
            {
                return Conflict();
            }
            return Ok(u);
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
