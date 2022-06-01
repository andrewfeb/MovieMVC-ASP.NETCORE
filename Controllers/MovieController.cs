using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieMVC.Controllers
{
    public class MovieController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public string GetMovie(int id)
        {
            return $"Get Movie with id: {id}";
        }

        [Route("[controller]/listmovie/{genre?}")]
        public string GetMovieByGenre(string genre)
        {
            return $"Get all movie with genre {genre}";
        }

        [HttpGet("[controller]/detail/{id?}")]
        public string Detail(int id)
        {
            return $"Get detail information for movie id {id}";
        }
    }
}
