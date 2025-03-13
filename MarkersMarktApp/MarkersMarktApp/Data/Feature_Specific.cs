using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakersMarktApp.Data
{
    internal class Feature_Specific
    {
        public int Id { get; set; }
        public int FeatureId { get; set; }
        public Feature Feature { get; set; }
        public int SpecificId { get; set; }
        public Specific Specific { get; set; }
    }
}
