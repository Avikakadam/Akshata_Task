using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Akshata_Task.Models;

namespace Akshata_Task.Controllers
{
    public class CategoryController : Controller
    {
        private List<Category> _categories;

        public CategoryController()
        {
            _categories = new List<Category>()
            {
                new Category { Id = 1, CategoryName = "Category 1" },
                new Category { Id = 2, CategoryName = "Category 2" },
                new Category { Id = 3, CategoryName = "Category 3" },
            };
        }

        // GET: Category
        public ActionResult Index()
        {
            return View(_categories);
        }


        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                category.Id = _categories.Count + 1;
                _categories.Add(category);
                return RedirectToAction("Index");
            }

            return View(category);
        }

        public ActionResult Edit(int id)
        {
            Category category = _categories.FirstOrDefault(c => c.Id == id);

            if (category == null)
            {
                return HttpNotFound();
            }

            return View(category);
        }

        [HttpPost]
        public ActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                Category oldCategory = _categories.FirstOrDefault(c => c.Id == category.Id);

                if (oldCategory == null)
                {
                    return HttpNotFound();
                }

                oldCategory.CategoryName = category.CategoryName;
                return RedirectToAction("Index");
            }

            return View(category);
        }

        public ActionResult Delete(int id)
        {
            Category category = _categories.FirstOrDefault(c => c.Id == id);

            if (category == null)
            {
                return HttpNotFound();
            }

            return View(category);
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Category category = _categories.FirstOrDefault(c => c.Id == id);

            if (category == null)
            {
                return HttpNotFound();
            }

            _categories.Remove(category);
            return RedirectToAction("Index");
        }
    }
}