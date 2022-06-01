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
    }
}
