using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace CS_Mgmt.Models
{
    internal class Supply
    {
        [PrimaryKey]
        public int SupplyId { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public string StorageLocation { get; set; }
    }
}
