using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkersMarktApp.Data
{
    internal class Sale
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int BuyerId { get; set; }
        public User Buyer { get; set; }
        public string Status { get; set; }
        public string Status_Description { get; set; }
        public DateTime DateSold { get; set; }
    }
}
