using RatingProductWebMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RatingProductWebMVC.Controllers
{
    public class ProductController : Controller
    {
        private RatingProductEntities dp = new RatingProductEntities();
       

        public ActionResult Index()
        {
            ViewBag.reviews = dp.Reviews.ToList();
            ViewBag.products = dp.Products.ToList();
            return View();
        }

        [HttpGet]
        public ActionResult Detail(int id)
        {
            var product = dp.Products.Find(id);
            ViewBag.products = product;
            var review = new Review()
            {
                ProductId = product.Id
            };

            ViewBag.reviews = dp.Reviews.ToList();
            return View("Detail" , review);
        }

        [HttpPost]
        public ActionResult SendReview(Review review , int rate)
        {
            string username = Session["username"].ToString();
            review.DatePost = DateTime.Now;
            review.AccountId = dp.Accounts.Single(a => a.UserName.Equals(username)).Id;
            review.Rating = rate;
            dp.Reviews.Add(review);
            dp.SaveChanges();
            return RedirectToAction("Detail", "Product" , new {id = review.ProductId});
        }

        [HttpGet]
        public ActionResult AddProduct()
        {
            //ViewBag.products = dp.Products;
            return View();
        }

        [HttpPost]
        public ActionResult AddProduct(string productname , Nullable<decimal> price ,string description, string photo)
        {
            var product = new Product() { ProductName = productname, Price = price, Description = description, Photo = photo };
            //ViewBag.products.Add(product);
            dp.Products.Add(product);
            dp.SaveChanges();
            return RedirectToAction("Index", "Product");
        }

    }
}