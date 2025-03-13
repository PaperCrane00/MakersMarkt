using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakersMarktApp.Data
{
    internal class Material_Specific
    {
        public int Id { get; set; }
        public int SpecificId { get; set; }
        public Specific Specific { get; set; }
        public int MaterialId { get; set; }
        public Material Material { get; set; }
    }
}
