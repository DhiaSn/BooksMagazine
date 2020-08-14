using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BooksMagazine.Data;
using BooksMagazine.Models.HomeModels;
using BooksMagazine.ViewModels;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace BooksMagazine.Controllers.HomeControllers
{
    public class CarouselItemsController : Controller
    {
        #region Constractor + Local Variable
        private readonly BooksMagazineContext _context;

        [Obsolete]
        public CarouselItemsController(BooksMagazineContext context , IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            Helpers.Methods.hostingEnvironment = hostingEnvironment;
        }
        #endregion

        #region CRUD
        #region GET
        // GET: CarouselItems
        public async Task<IActionResult> Index()
        {
            return View(await _context.CarouselItem.ToListAsync());
        }

        // GET: CarouselItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carouselItem = await _context.CarouselItem
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carouselItem == null)
            {
                return NotFound();
            }

            return View(carouselItem);
        }
        #endregion

        #region POST
        // GET: CarouselItems/Create
        public IActionResult Create()
        {
            return View();
        }
       
        // POST: CarouselItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCarouselItemViewModel carouselItem)
        {
            if(carouselItem.Type == null)
            {
                if (carouselItem.Image != null)
                    Helpers.Methods.CreateItem(carouselItem.Image);
                else
                    return NotFound();

                var item = CreateNewCarouselObject(carouselItem);

                if (ModelState.IsValid)
                {
                    _context.Add(item);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(item);
            }
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region PUT
        // GET: CarouselItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carouselItem = await _context.CarouselItem.FindAsync(id);
             
            if (carouselItem == null)
            {
                return NotFound();
            }
             
            return View(CreateCarouselItemViewModelObject(carouselItem));
        }

        // POST: CarouselItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CreateCarouselItemViewModel carouselItem)
        {
            #region Check Availability
            if (id != carouselItem.Id)
            {
                return NotFound();
            }
            #endregion

            #region Put Process
            if (carouselItem.Type == null)
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        #region Image Checking Process
                        if (carouselItem.Image != null)
                        {
                            if (carouselItem.Image.FileName != carouselItem.ImageLink)
                                Helpers.Methods.DeleteItem(carouselItem.ImageLink);

                            Helpers.Methods.CreateItem(carouselItem.Image);
                        }
                        #endregion

                        #region Save New Data
                        _context.Update(CreateNewCarouselObject(carouselItem));

                        await _context.SaveChangesAsync();
                        #endregion
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        #region Catching
                        if (!CarouselItemExists(carouselItem.Id))
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
                return View(carouselItem);
            }
            #endregion

            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Delete
        // GET: CarouselItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carouselItem = await _context.CarouselItem
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carouselItem == null)
            {
                return NotFound();
            }

            return View(carouselItem);
        }

        // POST: CarouselItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var carouselItem = await _context.CarouselItem.FindAsync(id);

            Helpers.Methods.DeleteItem(carouselItem.ImageLink);

            _context.CarouselItem.Remove(carouselItem);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        #endregion
        #endregion

        #region Methods
        #region CarouselItemExists
        private bool CarouselItemExists(int id)
        {
            return _context.CarouselItem.Any(e => e.Id == id);
        }
        #endregion

        #region CreateNewCarouselObject
        private CarouselItem CreateNewCarouselObject(CreateCarouselItemViewModel carouselItem)
        {
            return new CarouselItem()
            {
                Id = carouselItem.Id != default ? carouselItem.Id : carouselItem.Id,
                Title = carouselItem.Title,
                Quote = carouselItem.Quote,
                ImageLink = carouselItem.Image != null ? carouselItem.Image.FileName : carouselItem.ImageLink,
            };
        }
        #endregion

        #region CreateCarouselItemViewModelObject
        private CreateCarouselItemViewModel CreateCarouselItemViewModelObject(CarouselItem carouselItem)
        {
            return new CreateCarouselItemViewModel()
            {
                Id = carouselItem.Id,
                Title = carouselItem.Title,
                Quote = carouselItem.Quote,
                ImageLink = carouselItem.ImageLink
            };
        }
        #endregion
        #endregion
    }
}
