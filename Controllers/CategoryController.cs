using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieMVC.Repositories.Interfaces;
using MovieMVC.Models;

namespace MovieMVC.Controllers
{
    public class CategoryController : Controller
    {
        private ICategoryRepository _category;
        public CategoryController(ICategoryRepository category)
        {
            _category = category;
        }
        public IActionResult Index()
        {
            List<Category> categories = _category.GetAll().ToList();
            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            _category.Insert(category);

            return RedirectToAction("index");
        }

        public IActionResult Edit(int id)
        {
            Category category = _category.GetById(id);

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            _category.Update(category);
            return RedirectToAction("index");
        }
    }
}
