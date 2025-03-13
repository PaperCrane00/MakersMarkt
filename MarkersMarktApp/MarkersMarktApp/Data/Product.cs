using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkersMarktApp.Data
{
    internal class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image_Link { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int TypeId { get; set; }
        public Type Type { get; set; }
        public int SpecificId { get; set; }
        public Specific Specific { get; set; }
        public int Price { get; set; }
        public bool Is_Verified { get; set; }
        public bool Moderation { get; set; }
        public List<Sale> Sales { get; set; }
        public List<Product_Review> Reviews { get; set; }
    }
}
