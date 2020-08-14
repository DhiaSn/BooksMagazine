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
using BooksMagazine.ViewModels;
using System.IO;

namespace BooksMagazine.Controllers.HomeControllers
{
    public class TopicsController : Controller
    {
        #region Constractor + Local Variable
        private readonly BooksMagazineContext _context;
        [Obsolete]
        public TopicsController(BooksMagazineContext context , IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            Helpers.Methods.hostingEnvironment = hostingEnvironment;
        }
        #endregion

        #region CRUD
        #region GET
        // GET: Topics
        public async Task<IActionResult> Index()
        {
            return View(await _context.Topic.ToListAsync());
        }

        // GET: Topics/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var topic = await _context.Topic
                .FirstOrDefaultAsync(m => m.Id == id);
            if (topic == null)
            {
                return NotFound();
            }

            return View(topic);
        }
        #endregion

        #region POST
        // GET: Topics/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Topics/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateTopic_ViewModel topic)
        {
            if(topic.Type == null)
            {
                if(topic.Image != null)
                    Helpers.Methods.CreateItem(topic.Image);
                else
                    return NotFound();


                if (ModelState.IsValid)
                {
                    _context.Add(CreateNewTopicObject(topic));
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region PUT
        // GET: Topics/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var topic = await _context.Topic.FindAsync(id);
            if (topic == null)
            {
                return NotFound();
            }
            return View(CreateTopicViewModelObject(topic));
        }

        // POST: Topics/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CreateTopic_ViewModel topic)
        {
            #region Check Availability
            if (id != topic.Id)
            {
                return NotFound();
            }
            #endregion

            #region Put Process
            if (ModelState.IsValid)
            {
                try
                {
                    #region Image Checking Process
                    if (topic.Image != null)
                    {
                        if (topic.Image.FileName != topic.ImageLink)
                            Helpers.Methods.DeleteItem(topic.ImageLink);

                        Helpers.Methods.CreateItem(topic.Image);
                    }
                    #endregion

                    #region Save New Data
                    _context.Update(CreateNewTopicObject(topic));
                    await _context.SaveChangesAsync();
                    #endregion
                }
                
                catch (DbUpdateConcurrencyException)
                {
                    #region Catching
                    if (!TopicExists(topic.Id))
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
            #endregion

            return View(topic);
        }
        #endregion

        #region Delete
        // GET: Topics/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var topic = await _context.Topic
                .FirstOrDefaultAsync(m => m.Id == id);
            if (topic == null)
            {
                return NotFound();
            }

            return View(topic);
        }

        // POST: Topics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var topic = await _context.Topic.FindAsync(id);

            Helpers.Methods.DeleteItem(topic.ImageLink);

            _context.Topic.Remove(topic);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        #endregion
        #endregion

        #region Methods
        private bool TopicExists(int id)
        {
            return _context.Topic.Any(e => e.Id == id);
        }
        private Topic CreateNewTopicObject(CreateTopic_ViewModel topic)
        {
            return new Topic()
            {
                Id = topic.Id != default ? topic.Id : default,
                Title = topic.Title,
                Description = topic.Description,
                ImageLink = topic.Image.FileName
            };
        }
        private CreateTopic_ViewModel CreateTopicViewModelObject(Topic topic)
        {
            return new CreateTopic_ViewModel()
            {
                Id = topic.Id,
                Title = topic.Title,
                Description = topic.Description,
                ImageLink = topic.ImageLink
            };
        }
        #endregion
    }
}
