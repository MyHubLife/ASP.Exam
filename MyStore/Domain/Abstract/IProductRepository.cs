using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Abstract
{
    public interface IProductRepository
    {
        IEnumerable<Product> Products { get; }
        void SaveDish(Product product);     //метод интерфейса сохранение изменений
        Product DeleteDish(int Prod_Id);     //метод интерфейса удаление блюда
    }
}
