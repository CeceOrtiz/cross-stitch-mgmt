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

        public static List<Supply> GetSupplies(string dbPath)
        {
            using (var connection = new SQLiteConnection(dbPath))
            {
                List<Supply> allSupplies = connection.Table<Supply>().ToList();
                return allSupplies;
            }
        }
        public static Supply GetSelectedSupply(string dbPath, int supplyID)
        {
            using (var connection = new SQLiteConnection(dbPath))
            {
                Supply selectedSupply = connection.Table<Supply>().FirstOrDefault(s => s.SupplyId == supplyID);
                return selectedSupply;
            }
        }
    }
}
