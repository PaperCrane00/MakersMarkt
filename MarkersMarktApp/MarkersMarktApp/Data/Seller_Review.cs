using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkersMarktApp.Data
{
    internal class Seller_Review
    {
        public int Id { get; set; }
        public int SellerId { get; set; }
        public User Seller { get; set; }
        public int ReviewerId { get; set; }
        public User Reviewer { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; }
        public bool Moderation { get; set; }
    }
}
