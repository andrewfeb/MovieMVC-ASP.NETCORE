using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieMVC.Repositories.Interfaces;
using MovieMVC.Models;
using Microsoft.Extensions.Logging;

namespace MovieMVC.Controllers
{
    public class GenreController : Controller
    {
        private readonly IGenreRepository _genre;
        private readonly ILogger _logger;

        public GenreController(IGenreRepository genre, ILogger<GenreController> logger)
        {
            _genre = genre;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Genre> genres =  await _genre.GetAll();
            _logger.LogInformation("Get all genres");

            return View(genres.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Genre genre)
        {
            if (ModelState.IsValid)
            {
                await _genre.Insert(genre);
                _logger.LogInformation("Insert new genre");

                return RedirectToAction("index");
            }
            return View(genre);
        }

        public async Task<IActionResult> Edit(int id)
        {
            Genre genre = await _genre.GetById(id);

            return View(genre);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Genre genre)
        {
            if (ModelState.IsValid)
            {
                await _genre.Update(genre);
                _logger.LogInformation($"Update category with id {genre.Id}");

                return RedirectToAction("index");
            }
            return View(genre);
        }

        public async Task<IActionResult> Delete(Genre genre)
        {
            await _genre.Delete(genre);
            _logger.LogInformation($"Delete category with id {genre.Id}");

            return RedirectToAction("index");
        }
    }
}
