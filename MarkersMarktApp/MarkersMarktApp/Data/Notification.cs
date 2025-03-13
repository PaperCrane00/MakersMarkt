using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkersMarktApp.Data
{
    internal class Notification
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Created_at { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
