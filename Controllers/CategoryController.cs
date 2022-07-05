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
            List<Category> results = _category.GetAll().ToList();
            return View();
        }
    }
}
