using FitnessApp.Models;
using FitnessApp.Models.Data;
using FitnessApp.Models.StaticClasses;
using FitnessApp.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FitnessApp.Areas.Identity.Controllers
{
    [Area("Identity")]
    public class AccountController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;
        public AccountController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _context = context;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (ModelState.IsValid)
            {
                var user = new AppUser
                {
                    UserName = registerVM.Email,
                    Email = registerVM.Email,
                    NameSurname = registerVM.NameSurname,
                    PhoneNumber = registerVM.PhoneNumber,
                };
                var result = await _userManager.CreateAsync(user, registerVM.Password);
                if (result.Succeeded)
                {
                    if (!await _roleManager.RoleExistsAsync(Role.Role_Admin))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(Role.Role_Admin));
                    }
                    if (!await _roleManager.RoleExistsAsync(Role.Role_User))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(Role.Role_User));
                    }

                    await _userManager.AddToRoleAsync(user, Role.Role_Admin);

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Login");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(registerVM);
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            var result = await _signInManager.PasswordSignInAsync(loginVM.Email, loginVM.Password, loginVM.RememberMe, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(loginVM.Email);
                var roles = await _userManager.GetRolesAsync(user);

                if (roles.Contains(Role.Role_Admin))
                {
                    return RedirectToAction("UserList", "User", new { area = "Admin" });
                }
                else if (roles.Contains(Role.Role_User))
                {
                    return RedirectToAction("Index", "Home", new { area = "User" });
                }
                return RedirectToAction("Login");
            }

            return View(loginVM);
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
        public IActionResult AccessDenied()
        {
            return View();
        }   
        public async Task<IActionResult> Profil()
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var orderData = await _context.Order.Where(x => x.AppUserId == userId).Include(x=>x.Product).OrderByDescending(x => x.Date).AsNoTracking().ToListAsync();
            return View(orderData);
        }
        public async Task<IActionResult> OrderDetail(int id)
        {
            var data = await _context.Order.Where(x => x.Id == id).Include(x => x.AppUser).Include(x => x.Product).AsNoTracking().FirstOrDefaultAsync();
            return View(data);
        }
    }
}
