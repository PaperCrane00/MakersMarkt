using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakersMarktApp.Data
{
    internal class Specific
    {
        public int Id { get; set; }
        public TimeOnly Prod_Time { get; set; }
        public string Complexity { get; set; }
        public string Sustainability { get; set; }
        public List<Feature_Specific> Features { get; set; }
        public List<Material_Specific> Materials { get; set; }
        public List<Product> Products { get; set; }
    } 
}
