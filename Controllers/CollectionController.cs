using Collector.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Collector.Controllers
{
    public class CollectionController (ApplicationDbContext dbContext): Controller
    {
        // GET: CollectionController
        public async Task<IActionResult> Index()
        {
            return View(await dbContext.Collections.ToListAsync());
        }

        // GET: CollectionController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CollectionController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CollectionController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: CollectionController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CollectionController/Edit/5
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

        // GET: CollectionController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CollectionController/Delete/5
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
