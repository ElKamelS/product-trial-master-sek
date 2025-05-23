using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROJECT.Domain.Entities
{
    public class User
    {
        public int id { get; set; }
        public string username { get; set; }
        public string firstname { get; set; }
        public string email { get; set; }
        public string password { get; set; }

        public ICollection<Product> Wishlist { get; set; } = new List<Product>();
        public ICollection<Product> Cart { get; set; } = new List<Product>();
    }
}
