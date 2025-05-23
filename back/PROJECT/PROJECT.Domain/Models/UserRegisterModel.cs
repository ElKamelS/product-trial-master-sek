using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROJECT.Domain.Models
{
    public class UserRegisterModel
    {
        public string username { get; set; }
        public string firstname { get; set; }
        public string email { get; set; }
        public string password { get; set; }
    }
}
