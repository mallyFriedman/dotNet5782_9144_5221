using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class ProductItem
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
        public Category Category { get; set; }
        public int InStock { get; set; }
        public int AmountInCart { get; set; }

        public override string ToString() => $@"
        Product ID={Id}, 
        Product ProductName={ProductName}, 
    	Price: {Price}
        category - {Category}
        Amount in stock: {InStock}
        Amount in cart: {InStock}";
        
    }
}

