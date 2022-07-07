using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieMVC.Repositories.Interfaces;
using MovieMVC.Models;

namespace MovieMVC.Controllers
{
    public class GenreController : Controller
    {
        private readonly IGenreRepository _genre;

        public GenreController(IGenreRepository genre)
        {
            _genre = genre;
        }

        public IActionResult Index()
        {
            List<Genre> genres = _genre.GetAll().ToList();

            return View(genres);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Genre genre)
        {
            if (ModelState.IsValid)
            {
                _genre.Insert(genre);

                return RedirectToAction("index");
            }
            return View(genre);
        }

        public IActionResult Edit(int id)
        {
            Genre genre = _genre.GetById(id);

            return View(genre);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Genre genre)
        {
            if (ModelState.IsValid)
            {
                _genre.Update(genre);

                return RedirectToAction("index");
            }
            return View(genre);
        }

        public IActionResult Delete(Genre genre)
        {
            _genre.Delete(genre);

            return RedirectToAction("index");
        }
    }
}
