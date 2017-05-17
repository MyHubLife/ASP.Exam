using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Abstract;
using Domain.Entities;

namespace WebUI.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        IProductRepository repository;

        public AdminController(IProductRepository repo)
        {
            repository = repo;
        }

        public ViewResult Index()       //метод отображения главной страницы администрирования
        {
            return View(repository.Products);
        }

        public ViewResult Edit(int Prod_Id)  //метод редактирования блюд
        {
            Product product = repository.Products
                .FirstOrDefault(g => g.Prod_Id == Prod_Id);
            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Product product, HttpPostedFileBase image = null)
        {
            if (ModelState.IsValid)
            {
                // форма редактирования в которой нет картинок
                //repository.SaveDish(product); 
                //TempData["message"] = string.Format("Изменения блюда \"{0}\" были сохранены", product.Prod_Name);
                //return RedirectToAction("Index");

                //форма редактирования с картинками
                if (image != null)
                {
                    product.ImageMimeType = image.ContentType;
                    product.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(product.ImageData, 0, image.ContentLength);
                }
                repository.SaveDish(product);
                TempData["message"] = string.Format("Изменения блюда \"{0}\" было сохранено", product.Prod_Name);
                return RedirectToAction("Index");
            }
            else
            {
                // Что-то не так со значениями данных
                return View(product);
            }
        }

        public ViewResult Create()      //метод добавление блюд
        {
            return View("Edit", new Product());
        }

        [HttpPost]
        public ActionResult Delete(int Prod_Id)
        {
            Product deletedDish = repository.DeleteDish(Prod_Id);
            if (deletedDish != null)
            {
                TempData["message"] = string.Format("Блюдо \"{0}\" было удалено",
                    deletedDish.Prod_Name);
            }
            return RedirectToAction("Index");
        }
    }
}