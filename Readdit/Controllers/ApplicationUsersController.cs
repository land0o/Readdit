using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Readdit.Data;
using Readdit.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Readdit.Models.AppUserViewModel;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System;

namespace Bangazon.Controllers
{
    [Authorize]

    public class ApplicationUsersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        public ApplicationUsersController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IWebHostEnvironment environment)
        {
            _userManager = userManager;
            _context = context;
            _hostingEnvironment = environment;
        }

        //admin will need a view to get all users 

        //// GET: Users
        //public async Task<IActionResult> Index()
        //{
        //    var user = await _userManager.GetUserAsync(HttpContext.User);
        //    var User = new User()
        //    {
        //        FirstName = user.FirstName,
        //        LastName = user.LastName,
        //    };
        //    //var applicationDbContext = _context.User
        //    //                                    .Include(u => u.FirstName)
        //    //                                    .Where(u => u.UserId == u.UserId);
        //    return View(User);
        //}

        // GET: Users/Details/5
        public async Task<IActionResult> Details(string id)
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);

            ViewBag.CurentUserId = currentUser.Id;

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }
        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var UserViewModel = new UserImageViewModel()
            {
                User = user
            };

            if (id == null)
            {
                return NotFound();
            }


            user.Id = id;

            if (User == null)
            {
                return NotFound();
            }
            return View(UserViewModel);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, UserImageViewModel UserViewModel)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            //removed bind and only passing the info needed to update the db 
            user.FirstName = UserViewModel.User.FirstName;
            user.LastName = UserViewModel.User.LastName;
            user.Description = UserViewModel.User.Description;
            user.City = UserViewModel.User.City;
            user.imageUrl = UserViewModel.User.imageUrl;
            user.Email = UserViewModel.User.Email;

            //bug can not edit if an image is not uploaded need to grab the currentImage or move imageDelete lower
            if (id != user.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    var CurrentFileName = UserViewModel.User.imageUrl;
                    // check if the user added an image to save OR if the image added is different from the current one saved
                    if (UserViewModel.image != null || UserViewModel.image.FileName != CurrentFileName)
                    {
                        // get all of the images currently saved
                        var getAllImages = Directory.GetFiles("wwwroot/Images");
                        // if the current file name is not null
                        if (CurrentFileName != null)
                        {
                            // find the file to delete and store it in a variable
                            var fileToDelete = getAllImages.First(i => i.Contains(CurrentFileName));
                            // delete it 
                            System.IO.File.Delete(fileToDelete);
                        }
                        var UniqueFileName = GetUniqueFileName(UserViewModel.image.FileName);
                        var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "Images");
                        var filePath = Path.Combine(uploads, UniqueFileName);
                        using (var myFile = new FileStream(filePath, FileMode.Create))
                        {
                            UserViewModel.image.CopyTo(myFile);
                        }
                        user.imageUrl = UniqueFileName;
                    }

                    IdentityResult result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                        return RedirectToAction(nameof(Details));
                    else
                    {
                        foreach (IdentityError error in result.Errors)
                            ModelState.AddModelError("", error.Description);
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Details));
            }
            return View(user);
        }

        //admin will need a view to get Delete users 

        // GET: Users/Delete/5
        //public async Task<IActionResult> Delete(string id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var user = await _context.User
        //        .FirstOrDefaultAsync(m => m.UserId == id);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(user);
        //}

        // POST: Users/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(string id)
        //{
        //    var user = await _context.User.FindAsync(id);
        //    _context.User.Remove(user);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        private bool UserExists(string id)
        {
            return _context.ApplicationUsers.Any(e => e.Id == id);
        }
        private string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Path.GetFileNameWithoutExtension(fileName)
                      + "_"
                      + Guid.NewGuid().ToString().Substring(0, 4)
                      + Path.GetExtension(fileName);
        }
    }
}