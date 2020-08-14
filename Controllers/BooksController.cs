using BooksMagazine.Data;
using BooksMagazine.Models;
using BooksMagazine.ViewModels.BookViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BooksMagazine.Controllers
{
    public class BooksController : Controller
    {
        private readonly BooksMagazineContext _context;

        [System.Obsolete]
        public BooksController(BooksMagazineContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            Helpers.Methods.hostingEnvironment = hostingEnvironment;
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
            return View(await _context.Book.ToListAsync());
        }
        // GET: Books/Details/5
        public async Task<IActionResult> Type(string type)
        {
            if (type == string.Empty)
            {
                return NotFound();
            }

            var book = await _context.Book.Where(p => p.Type == type).ToListAsync();
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateBookViewModel book)
        {
            if (book.History == null)
            {
                if (book.History == null)
                {
                    if (book.BookFile != null && book.CoverImageFile != null)
                    {
                        Helpers.Methods.CreateItem(book.BookFile, true);
                        Helpers.Methods.CreateItem(book.CoverImageFile);
                    }
                    else
                        return NotFound();

                    var item = CreateNewBook(book);

                    _context.Add(item);
                    await _context.SaveChangesAsync();
                    return Redirect("/Home");
                }
            }
            return Redirect("/Home");
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Type,Title,AuthorName,CoverImage,Description,ReleaseDate")] Book book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.Id))
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
            return View(book);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .FirstOrDefaultAsync(m => m.Id == id);
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
            var book = await _context.Book.FindAsync(id);
            _context.Book.Remove(book);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return _context.Book.Any(e => e.Id == id);
        }

        private Book CreateNewBook(CreateBookViewModel book)
        {
            return new Book()
            {
                Id = book.Id != default ? book.Id : book.Id,
                AuthorName = book.AuthorName,
                Description = book.Description,
                ReleaseDate = book.ReleaseDate,
                Title = book.Title,
                Type = book.Type,
                CoverImage = book.CoverImageFile != null ? book.CoverImageFile.FileName : book.CoverImage,
                Link = book.BookFile != null ? book.BookFile.FileName : book.Link,
            };
        }
    }
}
