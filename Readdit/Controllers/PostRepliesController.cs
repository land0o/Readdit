using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Readdit.Data;
using Readdit.Models;

namespace Readdit.Controllers
{
    public class PostRepliesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        public PostRepliesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        // GET: PostReplies
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Replies.Include(p => p.Post).Include(p => p.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: PostReplies/Details/5
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

            var postReply = await _context.Replies
                .Include(p => p.Post)
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.PostReplyId == id);
            if (postReply == null)
            {
                return NotFound();
            }

            return View(postReply);
        }

        // GET: PostReplies/Create
        public async Task<IActionResult> Create(int postid)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            ViewData["PostId"] = postid;
            ViewData["UserId"] = user.Id;
            return View();
        }

        // POST: PostReplies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int postid, PostReply postReply)
        {

            ModelState.Remove("User");
            ModelState.Remove("UserId");
            ModelState.Remove("PostId");
            if (ModelState.IsValid)
            {
                _context.Add(postReply);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Posts", new { id = postReply.PostId });
            }
            var user = await _userManager.GetUserAsync(HttpContext.User);
            ViewData["PostId"] = postid;
            ViewData["UserId"] = user.Id;
            return View(postReply);
        }

        // GET: PostReplies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postReply = await _context.Replies.FindAsync(id);
            if (postReply == null)
            {
                return NotFound();
            }
            ViewData["PostId"] = new SelectList(_context.Posts, "PostId", "Message", postReply.PostId);
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", postReply.UserId);
            return View(postReply);
        }

        // POST: PostReplies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PostReplyId,DateCreated,Message,PostId,UserId")] PostReply postReply)
        {
            if (id != postReply.PostReplyId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(postReply);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostReplyExists(postReply.PostReplyId))
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
            ViewData["PostId"] = new SelectList(_context.Posts, "PostId", "Message", postReply.PostId);
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", postReply.UserId);
            return View(postReply);
        }

        // GET: PostReplies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postReply = await _context.Replies
                .Include(p => p.Post)
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.PostReplyId == id);
            if (postReply == null)
            {
                return NotFound();
            }

            return View(postReply);
        }

        // POST: PostReplies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var postReply = await _context.Replies.FindAsync(id);
            _context.Replies.Remove(postReply);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostReplyExists(int id)
        {
            return _context.Replies.Any(e => e.PostReplyId == id);
        }
    }
}
