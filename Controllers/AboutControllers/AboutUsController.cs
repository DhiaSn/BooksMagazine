using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BooksMagazine.Data;
using BooksMagazine.Models.AboutModels;
using Microsoft.AspNetCore.Hosting;
using BooksMagazine.ViewModels.AboutViewModels;

namespace BooksMagazine.Controllers.AboutControllers
{
    public class AboutUsController : Controller
    {
        #region Constractor + Local Variable
        private readonly BooksMagazineContext _context;
        [Obsolete]
        public AboutUsController(BooksMagazineContext context,IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            Helpers.Methods.hostingEnvironment = hostingEnvironment;
        }
        #endregion

        #region CRUD
        #region Get
        // GET: AboutUs
        public async Task<IActionResult> Index()
        {
            return View(await _context.AboutUs.ToListAsync());
        }

        // GET: AboutUs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aboutUs = await _context.AboutUs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aboutUs == null)
            {
                return NotFound();
            }

            return View(aboutUs);
        }
        #endregion

        #region PUT
        // GET: AboutUs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aboutUs = await _context.AboutUs.FindAsync(id);
            if (aboutUs == null)
            {
                return NotFound();
            }
            return View(CreateAboutUsViewModelObject(aboutUs));
        }

        // POST: AboutUs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Edit_AboutUS_ViewModel aboutUs)
        {
            if (id != aboutUs.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    #region Image Checking Process
                    if (aboutUs.Image != null)
                    {
                        if (aboutUs.Image.FileName != aboutUs.ImageLink)
                            Helpers.Methods.DeleteItem(aboutUs.ImageLink);

                        Helpers.Methods.CreateItem(aboutUs.Image);
                    }
                    #endregion

                    _context.Update(CreateNewAboutUsObject(aboutUs));
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AboutUsExists(aboutUs.Id))
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
            return View(aboutUs);
        }
        #endregion


        #region No Needed
        #region Post
        // GET: AboutUs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AboutUs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title")] AboutUs aboutUs)
        {
            if (ModelState.IsValid)
            {
                _context.Add(aboutUs);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(aboutUs);
        }
        #endregion

        #region Delete
        // GET: AboutUs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aboutUs = await _context.AboutUs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aboutUs == null)
            {
                return NotFound();
            }

            return View(aboutUs);
        }

        // POST: AboutUs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var aboutUs = await _context.AboutUs.FindAsync(id);
            _context.AboutUs.Remove(aboutUs);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        #endregion
        #endregion
        #endregion

        #region Methods
        private bool AboutUsExists(int id)
        {
            return _context.AboutUs.Any(e => e.Id == id);
        }
        
        private AboutUs CreateNewAboutUsObject(Edit_AboutUS_ViewModel aboutUs)
        {

            return new AboutUs()
            {
                Id = aboutUs.Id != default ? aboutUs.Id : default,
                Title = aboutUs.Title,
                Description = aboutUs.Description,
                ImageLink = aboutUs.Image != null ? aboutUs.Image.FileName : aboutUs.ImageLink,
            };
        }
        private Edit_AboutUS_ViewModel CreateAboutUsViewModelObject(AboutUs aboutUs)
        {
            return new Edit_AboutUS_ViewModel()
            {
                 Id = aboutUs.Id,
                 Title = aboutUs.Title,
                 Description = aboutUs.Description,
                 ImageLink = aboutUs.ImageLink
            };
        }
        #endregion
    }
}
