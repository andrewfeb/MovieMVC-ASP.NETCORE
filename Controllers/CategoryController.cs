using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieMVC.Repositories.Interfaces;
using MovieMVC.Models;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace MovieMVC.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _category;
        private readonly ILogger _logger;
        public CategoryController(ICategoryRepository category, ILogger<CategoryController> logger)
        {
            _category = category;
            _logger = logger;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            
            IEnumerable<Category> categories = await _category.GetAll();
            _logger.LogInformation("Show list categories");
            return View(categories.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            if (ModelState.IsValid)
            {
                await _category.Insert(category);
                _logger.LogInformation("Insert new category");

                return RedirectToAction("index");
            }
            return View(category);
        }

        public async Task<IActionResult> Edit(int id)
        {
            Category category = await _category.GetById(id);

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                await _category.Update(category);
                _logger.LogInformation($"Update category with id {category.Id}");

                return RedirectToAction("index");
            }
            return View(category);
        }
        
        public async Task<IActionResult> Delete(Category category)
        {
            await _category.Delete(category);
            _logger.LogInformation($"Delete category with id {category.Id}");

            return RedirectToAction("index");
        }

        public async Task<IActionResult> Detail(int id)
        {
            Category category = await _category.GetDetail(id);
            _logger.LogInformation($"Get detail category with id {id}");

            return View(category);
        }
    }
}
