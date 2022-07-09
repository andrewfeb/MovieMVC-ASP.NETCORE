using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieMVC.Repositories.Interfaces;
using MovieMVC.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using MovieMVC.Data;
using Microsoft.Extensions.Logging;

namespace MovieMVC.Controllers
{
    public class MovieController : Controller
    {
        private readonly IMovieRepository _movie;
        private readonly ICategoryRepository _category;
        private readonly IGenreRepository _genre;
        private readonly IWebHostEnvironment _env;
        private readonly ILogger _logger;
        
        public MovieController(IMovieRepository movie, ICategoryRepository category, IGenreRepository genre, IWebHostEnvironment env, ILogger<MovieController> logger)
        {
            _movie = movie;
            _category = category;
            _genre = genre;
            _env = env;
            _logger = logger;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Movie> movies = await _movie.GetAll();
            _logger.LogInformation("Get all movies");

            return View(movies.ToList());
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = new SelectList(await _category.GetAll(), "Id", "CategoryName");
            ViewBag.Genres = new MultiSelectList(await _genre.GetAll(), "Id", "GenreName");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MovieViewModel item)
        {
            if (ModelState.IsValid)
            {
                List<Genre> genres = new List<Genre>();

                foreach (int genreId in item.Genres)
                {
                    genres.Add(new Genre() { Id = genreId });
                }

                Movie movie = new Movie()
                {
                    Title = item.Title,
                    Year = item.Year,
                    Description = item.Description,
                    CategoryID = item.Category,
                    Genres = genres,
                    Cover = item.Cover.FileName
                };

                await UploadFile(item.Cover);

                await _movie.Insert(movie);
                _logger.LogInformation("Insert new movie");

                return RedirectToAction("index");
            }
            return View(item);
        }

        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.Categories = new SelectList(await _category.GetAll(), "Id", "CategoryName");
            ViewBag.Genres = new MultiSelectList(await _genre.GetAll(), "Id", "GenreName");

            Movie movie = await _movie.GetDetail(id);
            
            List<int> genres = new List<int>();
            foreach(Genre genre in movie.Genres)
            {
                genres.Add(genre.Id);
            }

            MovieViewModel movieViewModel = new MovieViewModel()
            {
                MovieId = movie.Id,
                Title = movie.Title,
                Category = movie.CategoryID,
                Description = movie.Description,
                Year = movie.Year,
                CoverName = movie.Cover,
                Genres = genres.ToArray()
            };

            return View(movieViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(MovieViewModel item)
        {
            if (ModelState.IsValid)
            {
                List<Genre> genres = new List<Genre>();

                foreach (int genreId in item.Genres)
                {
                    genres.Add(new Genre() { Id = genreId });
                }

                Movie movie = new Movie()
                {
                    Id = item.MovieId,
                    Title = item.Title,
                    Year = item.Year,
                    Description = item.Description,
                    CategoryID = item.Category,
                    Genres = genres,

                };

                movie.Cover = (item.Cover != null) ? item.Cover.FileName : item.CoverName;

                await UploadFile(item.Cover);

                await _movie.Update(movie);
                _logger.LogInformation($"Update movie with id {movie.Id}");

                return RedirectToAction("index");
            }
            return View(item);
        }

        public async Task<IActionResult> Delete(Movie movie)
        {
            await _movie.Delete(movie);
            _logger.LogInformation($"Delete movie with id {movie.Id}");

            return RedirectToAction("index");
        }

        public async Task<bool> UploadFile(IFormFile file)
        {
            if (file == null)
            {
                return false;
            }
            
            string uploads = Path.Combine(_env.ContentRootPath, @"Resources\Cover", file.FileName);
            if (file.Length > 0)
            {
                using FileStream fileStream = new FileStream(uploads, FileMode.Create);
                await file .CopyToAsync(fileStream);
            }

            return true;
        }
    }
}
