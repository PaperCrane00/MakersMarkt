﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakersMarktApp.Data
{
    internal class Role
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<Role_User> Users { get; set; }
    }
}
