﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakersMarktApp.Data
{
    public class Feature
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public List<FeatureSpecific> Specifics { get; set; }
    }
}
