using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bazingo_Core.Models
{
    public class Unit
    {
        public int UnitID { get; set; }
        public string Name { get; set; }
        public ICollection<ItemUnit> ItemUnits { get; set; }
    }
}
