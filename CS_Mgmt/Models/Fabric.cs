using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace CS_Mgmt.Models
{
    internal class Fabric
    {
        [PrimaryKey, AutoIncrement]
        public int FabricId { get; set; }
        public string Type { get; set; }
        public string Color { get; set; }
        public int Count { get; set; }
        public string StorageLocation { get; set; }
    }
}
