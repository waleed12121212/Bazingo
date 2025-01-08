using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bazingo_Core.Models
{
    public class Zone
    {
        public int ZoneID { get; set; }
        public string ZoneName { get; set; }
        public int CityID { get; set; }
        public City City { get; set; }
        public ICollection<Shipping> Shippings { get; set; }
    }

}
