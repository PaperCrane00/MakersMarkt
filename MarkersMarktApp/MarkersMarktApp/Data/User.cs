using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakersMarktApp.Data
{
    internal class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Credit {  get; set; }
        public bool Is_Verified { get; set; }
        public List<Role_User> Roles { get; set; }
        public List<Sale> Sales { get; set; }
        public List<Notification> Notifications { get; set; }
        public List<Product_Review> Product_Reviews { get; set; }
        public List<Product> Products { get; set; }
        public List<Seller_Review> Seller_Reviews { get; set; }
    }
}
