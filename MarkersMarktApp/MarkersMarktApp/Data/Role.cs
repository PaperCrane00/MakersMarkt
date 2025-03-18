using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakersMarktApp.Data
{
    public class Role
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<RoleUser> Users { get; set; }
    }
}
