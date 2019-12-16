﻿using System;
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
    public class PostsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        public PostsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        // GET: Posts for forum based off of Id
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Posts.Include(p => p.Forum).Include(p => p.User);
            return View(await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> ViewForumPost(Post postModel, int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts.Where(post => post.PostId == id)
                .Include(post => post.User)
                .Include(post => post.Forum)
                .Include(post => post.PostReplies)
                                .ThenInclude(reply => reply.User)
                .FirstOrDefaultAsync(m => m.PostId == id);

            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // GET: Posts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .Include(p => p.Forum)
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.PostId == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // GET: Posts/Create
        public IActionResult Create()
        {
            ViewData["ForumId"] = new SelectList(_context.Forums, "ForumId", "Description");
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id");
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PostId,DateCreated,Title,Message,UserId,ForumId")] Post post)
        {
            ModelState.Remove("User");
            if (ModelState.IsValid)
            {
                _context.Add(post);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ForumId"] = new SelectList(_context.Forums, "ForumId", "Description", post.ForumId);
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", post.UserId);
            return View(post);
        }

        // GET: Posts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            ViewData["ForumId"] = new SelectList(_context.Forums, "ForumId", "Description", post.ForumId);
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", post.UserId);
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PostId,DateCreated,Title,Message,UserId,ForumId")] Post post)
        {
            if (id != post.PostId)
            {
                return NotFound();
            }

            ModelState.Remove("User");
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(post);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.PostId))
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
            ViewData["ForumId"] = new SelectList(_context.Forums, "ForumId", "Description", post.ForumId);
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", post.UserId);
            return View(post);
        }

        // GET: Posts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .Include(p => p.Forum)
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.PostId == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostExists(int id)
        {
            return _context.Posts.Any(e => e.PostId == id);
        }
    }
}
