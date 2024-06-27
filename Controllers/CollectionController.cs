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

        // GET: /Collection/Details/id
        public async Task<IActionResult> Details(int id)
        {
            var collection = await dbContext.Collections.FindAsync(id);
            return View(collection);
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
                // Iterate through model state errors
                foreach (var modelStateEntry in ModelState.Values)
                {
                    foreach (var error in modelStateEntry.Errors)
                    {
                        // Log the error message to the console or a log file
                        Console.WriteLine($"Error: {error.ErrorMessage}");
                    }
                }

                ViewBag.Categories = new SelectList(dbContext.Categories, "Id", "Name", collection.CategoryId);
                return View(collection);
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
        public async Task<IActionResult> Delete(int id)
        {
            var collectionToDelete = await dbContext.Collections.FindAsync(id);
            if (collectionToDelete is null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(collectionToDelete);
        }

        // Delete: /Collection/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var collection = await dbContext.Collections.FindAsync(id);
            dbContext.Collections.Remove(collection);
            await dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
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
