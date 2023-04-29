using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using PagedList;
using PagedList.Mvc;
using Akshata_Task.Models;

namespace Akshata_Task.Controllers
{
    public class ProductController : Controller
    {
        private List<Product> _products;
        private List<Category> _categories;

        public ProductController()
        {
            _categories = new List<Category>()
            {
                new Category { Id = 1, CategoryName = "Category 1" },
                new Category { Id = 2, CategoryName = "Category 2" },
                new Category { Id = 3, CategoryName = "Category 3" },
            };

            _products = new List<Product>()
            {
                new Product { Id = 1, ProductName = "Product 1", CategoryId = 1, Category = _categories.FirstOrDefault(c => c.Id == 1) },
                new Product { Id = 2, ProductName = "Product 2", CategoryId = 2, Category = _categories.FirstOrDefault(c => c.Id == 2) },
                new Product { Id = 3, ProductName = "Product 3", CategoryId = 3, Category = _categories.FirstOrDefault(c => c.Id == 3) },
            };
        }

        // GET: Product
        public ActionResult Index(int? page, int pageSize = 10)
        {
            int pageNumber = (page ?? 1);

            var productList = from product in _products
                              join category in _categories on product.CategoryId equals category.Id
                              select new ProductViewModel
                              {
                                  Id = product.Id,
                                  ProductName = product.ProductName,
                                  CategoryId = category.Id,
                                  CategoryName = category.CategoryName
                              };

            var pagedProductList = productList.ToPagedList(pageNumber, pageSize);


            return View(pagedProductList);
        }

        public ActionResult Create()
        {
            ViewBag.Categories = new SelectList(_categories, "Id", "CategoryName");
            return View();
        }

        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                product.Id = _products.Count + 1;
                product.Category = _categories.FirstOrDefault(c => c.Id == product.CategoryId);
                _products.Add(product);
                return RedirectToAction("Index");
            }

            ViewBag.Categories = new SelectList(_categories, "Id", "CategoryName", product.CategoryId);
            return View(product);
        }

        public ActionResult Edit(int id)
        {
            Product product = _products.FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                return HttpNotFound();
            }

            ViewBag.Categories = new SelectList(_categories, "Id", "CategoryName", product.CategoryId);
            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                Product oldProduct = _products.FirstOrDefault(p => p.Id == product.Id);

                if (oldProduct == null)
                {
                    return HttpNotFound();
                }

                oldProduct.ProductName = product.ProductName;
                oldProduct.CategoryId = product.CategoryId;
                oldProduct.Category = _categories.FirstOrDefault(c => c.Id == product.CategoryId);
                return RedirectToAction("Index");
            }

            ViewBag.Categories = new SelectList(_categories, "Id", "CategoryName", product.CategoryId);
            return View(product);
        }




    }
}