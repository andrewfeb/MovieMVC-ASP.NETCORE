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

namespace MovieMVC.Controllers
{
    public class MovieController : Controller
    {
        private readonly IMovieRepository _movie;
        private readonly ICategoryRepository _category;
        private readonly IGenreRepository _genre;
        private readonly IWebHostEnvironment _env;
        private readonly MovieContext _context;
        
        public MovieController(IMovieRepository movie, ICategoryRepository category, IGenreRepository genre, IWebHostEnvironment env, MovieContext context)
        {
            _movie = movie;
            _category = category;
            _genre = genre;
            _env = env;
            _context = context;
        }
        public IActionResult Index()
        {
            List<Movie> movies = _movie.GetAll().ToList();
            
            return View(movies);
        }

        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(_category.GetAll(), "Id", "CategoryName");
            ViewBag.Genres = new MultiSelectList(_genre.GetAll(), "Id", "GenreName");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(MovieViewModel item)
        {
            List<Genre> genres = new List<Genre>();

            foreach(int genreId in item.Genres)
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

            UploadFile(item.Cover);

            _movie.Insert(movie);

            return RedirectToAction("index");
        }

        public IActionResult Edit(int id)
        {
            ViewBag.Categories = new SelectList(_category.GetAll(), "Id", "CategoryName");
            ViewBag.Genres = new MultiSelectList(_genre.GetAll(), "Id", "GenreName");

            Movie movie = _movie.GetDetail(id);
            
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
        public IActionResult Edit(MovieViewModel item)
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
            
            UploadFile(item.Cover);

            _movie.Update(movie);

            return RedirectToAction("index");
        }

        public IActionResult Delete(Movie movie)
        {
            _movie.Delete(movie);

            return RedirectToAction("index");
        }

        public void UploadFile(IFormFile file)
        {
            if (file != null)
            {
                string uploads = Path.Combine(_env.ContentRootPath, @"Resources\Cover", file.FileName);
                if (file.Length > 0)
                {
                    using FileStream fileStream = new FileStream(uploads, FileMode.Create);
                    file.CopyTo(fileStream);
                }
            }
        }
    }
}
