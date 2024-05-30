using System.Configuration;
using System.Net;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Collector.Data;
using Collector.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Collector.Controllers
{
    public class CollectionController (ApplicationDbContext dbContext, UserManager<IdentityUser> userManager, IConfiguration configuration): Controller
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
        public async Task<IActionResult> Create([Bind("Name,Description,CategoryId,ImageFile")] Collection collection)
        {
            if (!ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(collection.Name))
                    ModelState.AddModelError("Name", "The Name field is required.");
                if (string.IsNullOrEmpty(collection.Description))
                    ModelState.AddModelError("Description", "The Description field is required.");
                if (collection.CategoryId == 0)
                    ModelState.AddModelError("CategoryId", "The Category field is required.");
                ViewBag.Categories = new SelectList(dbContext.Categories, "Id", "Name", collection.CategoryId);
                return View(collection); // Return to the view with the model and validation errors
            }

            var user = await userManager.GetUserAsync(User);
            collection.UserId = user.Id;
            collection.CreatedDate = DateOnly.FromDateTime(DateTime.Now);

            if (collection.ImageFile.Length > 0)
            {
                collection.ImageUrl = await UploadImageToAzureBlobAsync(collection.ImageFile);
            } // Allowing no images
            
            dbContext.Collections.Add(collection);
            await dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
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

        private async Task<string> UploadImageToAzureBlobAsync(IFormFile imageFile)
        {
            string storageConnection = configuration.GetConnectionString("AzureContainerConnection") ??
                                       throw new Exception("Storage container connections string not found");
            var containerName = "images";
            var blobServiceClient = new BlobServiceClient(storageConnection);
            var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
            var blobName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
            var blobClient = containerClient.GetBlobClient(blobName);

            await using (var stream = imageFile.OpenReadStream())
            {
                await blobClient.UploadAsync(stream, new BlobHttpHeaders { ContentType = imageFile.ContentType });
            }
            
            return blobClient.Uri.ToString();
        }
    }
}
