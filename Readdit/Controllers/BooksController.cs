using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Readdit.Data;
using Readdit.Models;
using Readdit.Models.BooksViewModel;

namespace Readdit.Controllers
{
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpClientFactory _clientFactory;
        private readonly UserManager<ApplicationUser> _userManager;
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        private string _goodreadsApiKey = null;

        public IConfiguration Configuration { get; }

        public BooksController(ApplicationDbContext context, IHttpClientFactory clientFactory, IConfiguration configuration, UserManager<ApplicationUser> userManager)
        {
            _clientFactory = clientFactory;
            _context = context;
            Configuration = configuration;
            _userManager = userManager;
        }

        // GET: user Books
        public async Task<IActionResult> Index(Book book)
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);

            if (currentUser != null)
            {
                ViewBag.CurentUserId = currentUser.Id;
            }

            //ViewBag.

            var applicationDbContext = _context.Books.Include(b => b.User)
                .Where(b => b.User == currentUser);
            return View(await applicationDbContext.ToListAsync());
        }
        public async Task<IActionResult> Search(string SearchString, Book novel)
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);

            if (currentUser != null)
            {
                ViewBag.CurentUserId = currentUser.Id;
            }

            _goodreadsApiKey = Configuration["ApiKey"];
            var request = new HttpRequestMessage(HttpMethod.Get, $"?key={_goodreadsApiKey}&q={SearchString}");
            var client = _clientFactory.CreateClient("Goodreads");
            var response = await client.SendAsync(request);
            var bookXmlString = await response.Content.ReadAsStringAsync();

             XmlDocument bookAsXML = new XmlDocument();
             bookAsXML.LoadXml(bookXmlString);

            var BookJson = JsonConvert.SerializeXmlNode(bookAsXML, Newtonsoft.Json.Formatting.Indented);

            var deserializedBooks = JsonConvert.DeserializeObject<Rootobject>(BookJson); 

            List<Book> searchedBooks = new List<Book>();

            if (searchedBooks.Count() != 0)
            {
                var SingleResponse = deserializedBooks.GoodreadsResponse.search.results.work[0];

                ModelState.Remove("UserId");
                ModelState.Remove("User");
                if (ModelState.IsValid)
                {
                    novel.UserId = currentUser.Id;
                    novel.Title = SingleResponse.best_book.title;
                    novel.GoodreadsId = SingleResponse.id.text;
                    novel.Author = SingleResponse.best_book.author.name;
                    novel.Description = SingleResponse.average_rating;
                    novel.imageUrl = SingleResponse.best_book.image_url;
                    _context.Add(novel);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }

            }
            

            var responseArray = deserializedBooks.GoodreadsResponse.search.results;

            foreach (var book in responseArray.work)
            {
                Book newBook = new Book
                {
                    Title = book.best_book.title,
                    GoodreadsId = book.id.text,
                    Author = book.best_book.author.name,
                    Description = book.average_rating,
                    imageUrl = book.best_book.image_url,
                    UserId = currentUser.Id,
                };
                searchedBooks.Add(newBook);
            }

            return View(searchedBooks);
        }
        public async Task<IActionResult> SaveBooks(Book book)
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);

            if (currentUser != null)
            {
                ViewBag.CurentUserId = currentUser.Id;
            }

            ModelState.Remove("UserId");
            ModelState.Remove("User");
            if (ModelState.IsValid)
            {
                book.UserId = currentUser.Id;
                book.IsWish = true;
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", book.UserId);
            return View(book);
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .Include(b => b.User)
                .FirstOrDefaultAsync(m => m.BookId == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id");
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Book book)
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);

            if (currentUser != null)
            {
                ViewBag.CurentUserId = currentUser.Id;
            }

            ModelState.Remove("UserId");
            ModelState.Remove("User");
            if (ModelState.IsValid)
            {
                book.UserId = currentUser.Id;
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", book.UserId);
            return View(book);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", book.UserId);
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Book book)
        {
            if (id != book.BookId)
            {
                return NotFound();
            }
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);

            if (currentUser != null)
            {
                ViewBag.CurentUserId = currentUser.Id;
            }

            ModelState.Remove("UserId");
            ModelState.Remove("User");

            if (ModelState.IsValid)
            {
                book.UserId = currentUser.Id;
                try
                {
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.BookId))
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
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", book.UserId);
            return View(book);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .Include(b => b.User)
                .FirstOrDefaultAsync(m => m.BookId == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Books.FindAsync(id);
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.BookId == id);
        }
    }
}
