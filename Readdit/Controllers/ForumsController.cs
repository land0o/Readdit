using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Readdit.Data;
using Readdit.Models;

namespace Readdit.Controllers
{
    public class ForumsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        public ForumsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        // GET: All Forums created by users
        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);

            if (currentUser != null)
            {
                ViewBag.CurentUserId = currentUser.Id;
            }

            var applicationDbContext = _context.Forums.Include(f => f.User)
                                                                            .OrderByDescending(f => f.DateCreated);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Forums/Details/5
        public async Task<IActionResult> Details(int? id)
        {
      
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);

            if (currentUser != null)
            {
                ViewBag.CurentUserId = currentUser.Id;
            }

            if (id == null)
            {
                return NotFound();
            }

            var forum = await _context.Forums
                .Include(f => f.User)
                .Include(f => f.Posts)
                .FirstOrDefaultAsync(m => m.ForumId == id);

            var post = await _context.Posts.Where(post => post.PostId == id)
                .Include(post => post.User)
                .Include(post => post.Forum)
                .Include(post => post.PostReplies)
                                .ThenInclude(reply => reply.User)
                .FirstOrDefaultAsync(m => m.PostId == id);


            if (forum == null)
            {
                return NotFound();
            }

            return View(forum);
        }

        [Authorize]
        // GET: Forums/Create
        public IActionResult Create()
        {
            //ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id");
            return View();
        }

        // POST: Forums/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Forum forum)
        {
            ModelState.Remove("UserId");
            ModelState.Remove("User");

            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                forum.UserId = user.Id;
                _context.Add(forum);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", forum.UserId);
            return View(forum);
        }

        // GET: Forums/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var forum = await _context.Forums.FindAsync(id);
            if (forum == null)
            {
                return NotFound();
            }
            //ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", forum.UserId);
            return View(forum);
        }

        // POST: Forums/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Forum forum)
        {

            if (id != forum.ForumId)
            {
                return NotFound();
            }

            ModelState.Remove("User");
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);

            ViewBag.CurentUserId = currentUser.Id;
            if (ModelState.IsValid)
            {
                if (forum.UserId == currentUser.Id)
                {


                    try
                    {
                        _context.Update(forum);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ForumExists(forum.ForumId))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }


                    return RedirectToAction(nameof(Index));
                }
            }
            //ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", forum.UserId);
            return View(forum);
        }

        // GET: Forums/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var forum = await _context.Forums
                .Include(f => f.User)
                .FirstOrDefaultAsync(m => m.ForumId == id);
            if (forum == null)
            {
                return NotFound();
            }

            return View(forum);
        }

        // POST: Forums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var forum = await _context.Forums.FindAsync(id);
            _context.Forums.Remove(forum);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ForumExists(int id)
        {
            return _context.Forums.Any(e => e.ForumId == id);
        }
    }
}
