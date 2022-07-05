using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieMVC.Models;

namespace MovieMVC.Controllers
{
    public class MovieController : Controller
    {
        public IActionResult Index()
        {
            Movie movie = new Movie()
            {
                Id = 1,
                Title = "Avengers Endgame",
                Year = "2019",
                Genres = new List<Genre>()
                {
                    new Genre(){Id = 1, GenreName="Action"},
                    new Genre(){Id = 2, GenreName="Sci-fi"}
                },
                Cover = "Avengers_Endgame.jpg",
                Description = "After the devastating events of Avengers: Infinity War (2018), the universe is in ruins. With the help of remaining allies, the Avengers assemble once more in order to reverse Thanos' actions and restore balance to the universe."
            };

            return View(movie);
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
