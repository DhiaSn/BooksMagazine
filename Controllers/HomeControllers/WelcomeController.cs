using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BooksMagazine.Data;
using BooksMagazine.Models.HomeModels;
using Microsoft.AspNetCore.Hosting;
using BooksMagazine.ViewModels.HomeViewModels;
using BooksMagazine.Models;

namespace BooksMagazine.Controllers.HomeControllers
{
    public class WelcomeController : Controller
    {
        #region Constractor + Local Variable
        private readonly BooksMagazineContext _context;
        [Obsolete]
        public WelcomeController(BooksMagazineContext context , IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            Helpers.Methods.hostingEnvironment = hostingEnvironment;
        }
        #endregion

        #region CRUD
        #region GET
        // GET: Welcome
        public async Task<IActionResult> Index()
        {
            return View(await _context.Welcome.Include(p=>p.Purposes)
                                              .ToListAsync());
        }

        // GET: Welcome/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var welcome = await _context.Welcome.Include(p => p.Purposes)
                                                .FirstOrDefaultAsync(m => m.Id == id);
            if (welcome == null)
            {
                return NotFound();
            }

            return View(welcome);
        }
        #endregion

        #region POST
        // GET: Welcome/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Welcome/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Welcome_Create_ViewModel welcome)
        {
            if(welcome.Type == null)
            {
                if (welcome.Image != null)
                    Helpers.Methods.CreateItem(welcome.Image);
                else
                    return NotFound();

                if (ModelState.IsValid)
                {
                    _context.Add(CreateNewWelcomeObject(welcome));
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region PUT
        // GET: Welcome/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var welcome = await _context.Welcome.Include(p => p.Purposes).FirstOrDefaultAsync(m => m.Id == id);
            if (welcome == null)
            {
                return NotFound();
            }
            return View(CreateWelcomeViewModelObject(welcome));
        }

        // POST: Welcome/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,Welcome_Create_ViewModel welcome)
        {
            if (id != welcome.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    #region Image Checking Process
                    if (welcome.Image != null)
                    {
                        if (welcome.Image.FileName != welcome.ImageLink)
                            Helpers.Methods.DeleteItem(welcome.ImageLink);

                        Helpers.Methods.CreateItem(welcome.Image);
                    }
                    #endregion

                    #region Save New Data
                    _context.Update(CreateNewWelcomeObject(welcome));
                    await _context.SaveChangesAsync();
                    #endregion
                }
                catch (DbUpdateConcurrencyException)
                {
                    #region Catching
                    if (!WelcomeExists(welcome.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                    #endregion
                }
                return RedirectToAction(nameof(Index));
            }
            return View(welcome);
        }
        #endregion

        #region DELETE
        // GET: Welcome/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var welcome = await _context.Welcome
                                        .Include(p=>p.Purposes)
                                        .FirstOrDefaultAsync(m => m.Id == id);
            if (welcome == null)
            {
                return NotFound();
            }

            return View(welcome);
        }

        // POST: Welcome/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var welcome = await _context.Welcome
                                        .Include(p => p.Purposes)
                                        .FirstOrDefaultAsync(m => m.Id == id);

            Helpers.Methods.DeleteItem(welcome.ImageLink);

            _context.Welcome.Remove(welcome);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        #endregion
        #endregion

        #region Methods
        private bool WelcomeExists(int id)
        {
            return _context.Welcome.Any(e => e.Id == id);
        }
        private Welcome CreateNewWelcomeObject(Welcome_Create_ViewModel welcome)
        {

            return new Welcome()
            {
                Id = welcome.Id != default ? welcome.Id : default,
                Title = welcome.Title,
                Description = welcome.Description,
                GoalTitle = welcome.GoalTitle,
                ImageLink = welcome.Image != null ? welcome.Image.FileName : welcome.ImageLink,
                Purposes = new List<Purpose>(welcome.Purposes)
            };
        }
        private Welcome_Create_ViewModel CreateWelcomeViewModelObject(Welcome welcome)
        {
            return new Welcome_Create_ViewModel()
            {
                Id = welcome.Id,
                Title = welcome.Title,
                Description = welcome.Description,
                GoalTitle = welcome.GoalTitle,
                ImageLink = welcome.ImageLink,
                Purposes = welcome.Purposes
            };
        }
        #endregion
    }
}
