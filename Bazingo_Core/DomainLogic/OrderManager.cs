using Bazingo_Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bazingo_Core.DomainLogic
{
    public class OrderManager
    {
        public decimal CalculateTotalAmount(IEnumerable<OrderDetail> orderDetails , decimal taxRate)
        {
            var subtotal = orderDetails.Sum(od => od.PricePerUnit * od.Quantity);
            var tax = subtotal * taxRate;
            return subtotal + tax;
        }

        public bool ValidateStockAvailability(IEnumerable<OrderDetail> orderDetails , Dictionary<int , int> productStocks)
        {
            foreach (var detail in orderDetails)
            {
                if (!productStocks.ContainsKey(detail.ProductID) || productStocks[detail.ProductID] < detail.Quantity)
                {
                    return false;
                }
            }
            return true;
        }
    }

}
