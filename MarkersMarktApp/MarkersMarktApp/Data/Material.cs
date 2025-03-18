using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakersMarktApp.Data
{
    public class Material
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<MaterialSpecific> Specifics { get; set; }
    } 
}
