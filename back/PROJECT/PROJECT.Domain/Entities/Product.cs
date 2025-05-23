using PROJECT.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROJECT.Domain.Entities
{
    public class Product
    {
        public int id { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string image { get; set; }
        public string category { get; set; }
        public int price { get; set; }
        public int quantity { get; set; }
        public string internalReference { get; set; }
        public int shellId { get; set; }
        [EnumDataType(typeof(InventoryStatusEnum))]
        public InventoryStatusEnum InventoryStatus { get; set; }
        public int rating { get; set; }
        public int createdAt { get; set; }
        public int updatedAt { get; set; }

        public ICollection<User> UsersWishlist { get; set; } = new List<User>();
        public ICollection<User> UsersCart { get; set; } = new List<User>();
    }
}
