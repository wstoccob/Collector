using Collector.Data;
using Collector.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Collector.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminPanelController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;

        public AdminPanelController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            var usersDto = new List<UserDto>();
            foreach (var user in users)
            {
                var userDto = new UserDto();
                userDto.UserId = user.Id;
                userDto.Email = user.Email;
                userDto.Roles = await _userManager.GetRolesAsync(user);
                usersDto.Add(userDto);
            }
            return View(usersDto);
        }
    }
}
