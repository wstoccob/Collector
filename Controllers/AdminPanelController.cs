using Collector.Data;
using Collector.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Collector.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminPanelController(UserManager<IdentityUser> userManager, ApplicationDbContext dbContext) : Controller
    {
        //GET: AdminPanel
        public async Task<IActionResult> Index()
        {
            var users = await userManager.Users.ToListAsync();
            var usersDto = new List<UserViewModel>();
            foreach (var user in users)
            {
                var userDto = new UserViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName!,
                    Email = user.Email!,
                    Roles = await userManager.GetRolesAsync(user)
                };
                usersDto.Add(userDto);
            }
            return View(usersDto);
        }

        // POST: Jokes/ChangeRole/username
        [HttpPost]
        public async Task<IActionResult> ChangeRole(string username)
        {
            var user = await userManager.FindByNameAsync(username);
            // userManager.
            return View();
        }
    }
}
