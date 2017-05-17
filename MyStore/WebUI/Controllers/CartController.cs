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
    public class CartController : Controller
    {
        private IProductRepository repository;
        private IOrderProcessor orderProcessor; //електронная почтв
                                                //public CartController(IProductRepository repo)
                                                //{
                                                //    repository = repo;
                                                //}

        public CartController(IProductRepository repo, IOrderProcessor processor)
        {
            repository = repo;
            orderProcessor = processor;
        }

        // обработка заказа через почту

        public ViewResult Checkout()
        {
            return View(new ShippingDetails());
        }

        [HttpPost]
        public ViewResult Checkout(Cart cart, ShippingDetails shippingDetails)
        {
            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Извините, но Ваша корзина пуста! Сделайте Ваш выбор!");
            }

            if (ModelState.IsValid)
            {
                orderProcessor.ProcessOrder(cart, shippingDetails);
                cart.Clear();
                return View("Completed");
            }
            else
            {
                return View(shippingDetails);
            }
        }

        public ViewResult Index(Cart cart, string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                //Cart = GetCart(),
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }
        
        public RedirectToRouteResult AddToCart(Cart cart, int prod_id, string returnUrl)
        {
            Product prod = repository.Products
                .FirstOrDefault(g => g.Prod_Id == prod_id);

            if (prod != null)
            {
                cart.AddItem(prod, 1);
                //GetCart().AddItem(prod, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(Cart cart, int prod_id, string returnUrl)
        {
            Product prod = repository.Products
                .FirstOrDefault(g => g.Prod_Id == prod_id);

            if (prod != null)
            {
                cart.RemoveLine(prod);
                //GetCart().RemoveLine(prod);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        //public Cart GetCart()
        //{
        //    Cart cart = (Cart)Session["Cart"];
        //    if (cart == null)
        //    {
        //        cart = new Cart();
        //        Session["Cart"] = cart;
        //    }
        //    return cart;
        //}

        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }

        //public ViewResult Checkout(Cart cart, ShippingDetails shippingDetails)   этот мнтод указан выше
        //{
        //    return View(new ShippingDetails());
        //}
    }
}