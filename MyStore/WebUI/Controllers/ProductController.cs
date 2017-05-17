using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Abstract;
using Domain.Entities;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository repository;
        public int pageSize = 4;
        public ProductController(IProductRepository repo)
        {
            repository = repo;
        }
        public ViewResult List(string category, int page = 1)
        {
            // метод отображения без использования ХТМЛхелпера
            //return View(repository.Products       
            //    .OrderBy(prod => prod.Prod_Id)
            //    .Skip((page -1)*pageSize)
            //    .Take(pageSize));

            //Добавление данных модели представления!!!!!!!!

            ProductsListViewModel model = new ProductsListViewModel
            {
                Products = repository.Products
                        .Where(prod => category == null || prod.Prod_Category == category)
                        .OrderBy(prod => prod.Prod_Id)
                        .Skip((page - 1) * pageSize)
                        .Take(pageSize),
                    PagingInfo = new PagingInfo
                    {
                        CurrentPage = page,
                        ItemsPerPage = pageSize,
                        //TotalItems = repository.Products.Count()
                        TotalItems = category == null ?
                        repository.Products.Count():repository.Products.Where(prod => prod.Prod_Category == category).Count()
                    },
                    CurrentCategory = category
                };
            return View(model);
    }
        public FileContentResult GetImage(int Prod_Id)
        {
            Product product = repository.Products
                .FirstOrDefault(g => g.Prod_Id == Prod_Id);

            if (product != null)
            {
                return File(product.ImageData, product.ImageMimeType);
            }
            else
            {
                return null;
            }
        }
    }
}