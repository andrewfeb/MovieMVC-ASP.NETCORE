using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieMVC.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;

namespace MovieMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _usermanager;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger _logger;

        public AccountController(UserManager<User> userManager, SignInManager<User> signinManager, ILogger<AccountController> logger)
        {
            _usermanager = userManager;
            _signInManager = signinManager;
            _logger = logger;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _usermanager.FindByEmailAsync(loginViewModel.Email);

                if (user != null && await _usermanager.CheckPasswordAsync(user, loginViewModel.Password))
                {
                    var identity = new ClaimsIdentity(IdentityConstants.ApplicationScheme);
                    identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id));
                    identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));

                    await HttpContext.SignInAsync(IdentityConstants.ApplicationScheme, new ClaimsPrincipal(identity));

                    _logger.LogInformation("Login successfull");
                    if (Request.Query.Keys.Contains("ReturnUrl"))
                    {
                        return Redirect(Request.Query["ReturnUrl"].First());
                    }
                    else
                    {
                        return RedirectToAction("index", "home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Error Email or Password");
                    _logger.LogInformation("Error email or password");
                    return View();
                }
            }

            return View(loginViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOut()
        {
            if (User.Identity.IsAuthenticated)
            {
                await _signInManager.SignOutAsync();
            }

            return RedirectToAction("Login");
        }
    }
}
