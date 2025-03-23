using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AdFit.Core.Model
{
    public enum ERole
    {
        ADMIN,
        NEWSPAPER_OWNER,
        USER
    }
    public class User
    {
        public int Id { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public List<Advertisement> Advertisements { get; set; } = new List<Advertisement>(); 

        public ERole Role { get; set; }
    }
}
