using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieMVC.Services;

namespace MovieMVC.Controllers
{
    public class HomeController : Controller
    {
        private IRandomService _randomService;
        private IRandomWrapper _randomWrapper;

        public HomeController(IRandomService randomService, IRandomWrapper randomWrapper)
        {
            _randomService = randomService;
            _randomWrapper = randomWrapper;
        }

        public IActionResult Index()
        {
            string result = $"The number from service in controller: { _randomService.GetNumber()}, the number from wrapper service: { _randomWrapper.GetNumber()}";

            return Content(result);
        }
    }
}
