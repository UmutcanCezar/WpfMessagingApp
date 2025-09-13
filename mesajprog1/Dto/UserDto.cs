using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mesajprog1.Dto
{
    class UserDto
    {

        public int id { get; set; }
        public string username { get; set; }
        public string emailAddress { get; set; }
        public string password { get; set; }
        public string? profilePictureUrl { get; set; }
    }
}
