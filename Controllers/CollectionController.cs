using System.Net;
using Collector.Data;
using Collector.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Collector.Controllers
{
    public class CollectionController (ApplicationDbContext dbContext, UserManager<IdentityUser> userManager): Controller
    {
        // GET: /Collection
        public async Task<IActionResult> Index()
        {
            return View(await dbContext.Collections.ToListAsync());
        }

        // GET: /Collection/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: /Collection/Create
        [Authorize]
        public async Task<IActionResult> Create()
        {
            var categories = await dbContext.Categories
                .Select(c => new SelectListItem 
                {
                    Value = c.Id.ToString(), 
                    Text = c.Name 
                })
                .ToListAsync();
            ViewBag.Categories = categories;

            return View();
        }

        // POST: /Collection/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Name,Description,CategoryId,ImageUrl")] 
            Collection collection, 
            IFormFile imageFile)
        {
            if (ModelState.IsValid && imageFile.Length > 0)
            {
                
                
                
                var user = await userManager.GetUserAsync(User);
                collection.UserId = await userManager.GetUserIdAsync(user!);
                collection.CreatedDate = DateOnly.FromDateTime(DateTime.Now);
                dbContext.Add(collection);
                await dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Categories = new SelectList(dbContext.Categories, "Id", "Name", collection.CategoryId);
            return View(collection);
        }

        // GET: /Collection/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: /Collection/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: /Collection/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: /Collection/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
