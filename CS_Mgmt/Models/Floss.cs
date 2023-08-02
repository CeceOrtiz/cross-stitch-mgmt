using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace CS_Mgmt.Models
{
    internal class Floss
    {
        [PrimaryKey]
        public int FlossId { get; set; }
        public string StandardName { get; set; }
        public string Color { get; set; }
    }
}
