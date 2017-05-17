using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Cart
    {
        private List<CartLine> LineCollection = new List<CartLine>();

        public void AddItem(Product product, int quantity)
        {
            CartLine Line = LineCollection
                .Where(g => g.product.Prod_Id == product.Prod_Id)
                .FirstOrDefault();

            if (Line == null)
            {
                LineCollection.Add(new CartLine
                {
                    product = product,
                    Quantity = quantity
                });
            }
            else
            {
                Line.Quantity += quantity;
            }
        }
        public void RemoveLine(Product product)
        {
            LineCollection.RemoveAll(l => l.product.Prod_Id == product.Prod_Id);
        }

        public decimal ComputeTotalValue()
        {
            return LineCollection.Sum(e => e.product.Prod_Price * e.Quantity);

        }
        public void Clear()
        {
            LineCollection.Clear();
        }

        public IEnumerable<CartLine> Lines
        {
            get { return LineCollection; }
        }
    }

    public class CartLine
    {
        public Product product { get; set; }
        public int Quantity { get; set; }
    }
}