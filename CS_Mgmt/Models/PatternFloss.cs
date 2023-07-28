using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace CS_Mgmt.Models
{
    internal class PatternFloss
    {
        [PrimaryKey]
        public string CompositeKey { get; set; }
        public int PatternId { get; set; }
        public int FlossId { get; set; }
        public int SkeinsNeeded { get; set; }
    }
}
