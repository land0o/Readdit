using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Readdit.Data;
using Readdit.Models;

namespace Readdit.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;


        public HomeController(ILogger<HomeController> logger, UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _logger = logger;
            _context = context;
        }
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        public async Task <IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);

            if (currentUser != null)
            {
                ViewBag.CurentUserId = currentUser.Id;
            }

            var applicationDbContext = _context.Forums.OrderByDescending(p => p.DateCreated).Take(5);

            return View(await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> Search(string SearchString)
        {
            var applicationDbContext = _context.Forums.Where(f => f.Title.ToLower().Contains(SearchString) || f.Description.ToLower().Contains(SearchString));

            return View(await applicationDbContext.ToListAsync());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
