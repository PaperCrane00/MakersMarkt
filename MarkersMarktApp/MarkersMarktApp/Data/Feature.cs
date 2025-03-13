using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkersMarktApp.Data
{
    internal class Feature
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public List<Feature_Specific> Specifics { get; set; }
    }
}
