using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakersMarktApp.Data
{
    public class Specific
    {
        public int Id { get; set; }
        public TimeOnly ProdTime { get; set; }
        public string Complexity { get; set; }
        public string Sustainability { get; set; }
        public List<FeatureSpecific> Features { get; set; }
        public List<MaterialSpecific> Materials { get; set; }
    } 
}
