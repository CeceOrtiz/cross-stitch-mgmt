﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace CS_Mgmt.Models
{
    internal class UserFloss
    {
        [PrimaryKey]
        public int FlossId { get; set; }
        public int Quantity { get; set; }
        public string StorageLocation { get; set; }

        
    }
}
