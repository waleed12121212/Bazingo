using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bazingo_Core.Models
{
    public class ItemUnit
    {
        public int ItemUnitID { get; set; }
        public int ProductID { get; set; }
        public Product Product { get; set; }
        public int UnitID { get; set; }
        public Unit Unit { get; set; }
        public int QuantityPerUnit { get; set; }
    }
}
