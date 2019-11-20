using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weather.DataAccess;

namespace Weather.Models
{
    class User
    {
        public string Name { get; set; }
        public int DefaultZip { get; set; }
        public int MeasurmentSystem { get; set; }
        public int FontId { get; set; }
        public int Id = 0;

        public void UpdateDb()
        {
            SqliteDb.UpdateUser(this);
        }
    }
}
