using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bazingo_Application.DTOs.Carts
{
    public class CartUpdateDTO
    {
        public int ProductID { get; set; }
        public int Quantity { get; set; }
    }
}
