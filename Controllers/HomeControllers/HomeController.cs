using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BooksMagazine.Data;
using BooksMagazine.Models.HomeModels;

namespace BooksMagazine.Controllers
{
    public class HomeController : Controller
    {
        #region Contractor + Local Variable
        private readonly BooksMagazineContext _context;

        public HomeController(BooksMagazineContext context)
        {
            _context = context;
        }
        #endregion

        #region CRUD
        #region GET    
        // GET: Home
        public async Task<IActionResult> Index()
        {
            GetbooksAvailableType();

            return View(await _context.Home.Include(h => h.CarouselItems)
                                           .Include(h => h.Welcome)
                                           .Include(h => h.Welcome.Purposes)
                                           .Include(h => h.Topics)
                                           .ToListAsync());

        }
        
        // GET: Home/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var home = await _context.Home.Include(h => h.CarouselItems)
                                          .Include(h => h.Welcome)
                                          .Include(h => h.Welcome.Purposes)
                                          .Include(h => h.Topics)
                                          .FirstOrDefaultAsync(m => m.Id == id);
            if (home == null)
            {
                return NotFound();
            }

            return View(home);
        }
        #endregion

        #region POST
        // GET: Home/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Home/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id")] Home home)
        {
            if (ModelState.IsValid)
            {
                _context.Add(home);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(home);
        }

        #endregion

        #region PUT
        // GET: Home/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var home = await _context.Home.FindAsync(id);
            if (home == null)
            {
                return NotFound();
            }
            return View(home);
        }

        // POST: Home/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id")] Home home)
        {
            if (id != home.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(home);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HomeExists(home.Id))
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
            return View(home);
        }

        #endregion

        #region DELETE
        // GET: Home/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var home = await _context.Home
                .FirstOrDefaultAsync(m => m.Id == id);
            if (home == null)
            {
                return NotFound();
            }

            return View(home);
        }

        // POST: Home/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var home = await _context.Home.FindAsync(id);
            _context.Home.Remove(home);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        #endregion
        #endregion

        #region Methods
        private bool HomeExists(int id)
        {
            return _context.Home.Any(e => e.Id == id);
        }
        private void GetbooksAvailableType()
        {
            Helpers.Constants.BooksTypeName = _context.Book.Select(b => b.Type).Distinct().ToList();
        }
        #endregion
    }
}
