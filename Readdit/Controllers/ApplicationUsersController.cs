using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Readdit.Data;
using Readdit.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Bangazon.Controllers
{
    [Authorize]

    public class ApplicationUsersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        public ApplicationUsersController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _context = context;
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
        public async Task<IActionResult> Details()
        {

            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }
        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(HttpContext.User);

            user.Id = id;

            if (User == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, ApplicationUser applicationUser)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            //removed bind and only passing the info needed to update the db 
            user.FirstName = applicationUser.FirstName;
            user.LastName = applicationUser.LastName;
            user.Description = applicationUser.Description;
            user.City = applicationUser.City;
            user.imageUrl = applicationUser.imageUrl;
            user.Email = applicationUser.Email;


            if (id != user.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    //
                    // Summary:
                    //     Updates the specified user in the backing store.
                    //
                    // Parameters:
                    //   user:
                    //     The user to update.
                    //
                    // Returns:
                    //     The System.Threading.Tasks.Task that represents the asynchronous operation, containing
                    //     the Microsoft.AspNetCore.Identity.IdentityResult of the operation.
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
    }
}