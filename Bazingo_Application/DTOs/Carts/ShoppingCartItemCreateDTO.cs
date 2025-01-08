using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bazingo_Application.DTOs.Carts
{
    public class ShoppingCartItemCreateDTO
    {
        [Required]
        public string BuyerID { get; set; }

        [Required]
        public int ProductID { get; set; }

        [Required, Range(1 , int.MaxValue)]
        public int Quantity { get; set; }
    }
}
