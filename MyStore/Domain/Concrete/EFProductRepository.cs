using Domain.Abstract;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Concrete
{
    public class EFProductRepository : IProductRepository
    {
        EFDbContext context = new EFDbContext();
        public IEnumerable<Product> Products
        {
            get { return context.Products; }
        }

        public void SaveDish(Product product)       // метод сохранения инфо о блюде в базе данных
        {
            if (product.Prod_Id == 0)
                context.Products.Add(product);
            else
            {
                Product dbEntry = context.Products.Find(product.Prod_Id);
                if (dbEntry != null)
                {
                    dbEntry.Prod_Name = product.Prod_Name;
                    dbEntry.Prod_Description = product.Prod_Description;
                    dbEntry.Prod_Price = product.Prod_Price;
                    dbEntry.Prod_Category = product.Prod_Category;
                    dbEntry.ImageData = product.ImageData;
                    dbEntry.ImageMimeType = product.ImageMimeType;
                }
            }
            context.SaveChanges();
        }

        public Product DeleteDish(int Prod_Id)       //метод удалнеия блюда с базі данных
        {
            Product dbEntry = context.Products.Find(Prod_Id);
            if (dbEntry != null)
            {
                context.Products.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
    
}
