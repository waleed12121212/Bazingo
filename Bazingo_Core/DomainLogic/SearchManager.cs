using Bazingo_Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bazingo_Core.DomainLogic
{
    public class SearchManager
    {
        public IEnumerable<Product> SearchProducts(IEnumerable<Product> products , string keyword , string category = null)
        {
            var filteredProducts = products.Where(p => p.ProductName.Contains(keyword , StringComparison.OrdinalIgnoreCase) ||
                                                       p.Description.Contains(keyword , StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrEmpty(category))
            {
                filteredProducts = filteredProducts.Where(p => p.Category.CategoryName.Equals(category , StringComparison.OrdinalIgnoreCase));
            }

            return filteredProducts;
        }
    }

}
