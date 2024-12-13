using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolManagmen.Models;

namespace SchoolManagmen.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<Account> _userManager;
        private readonly SignInManager<Account> _signInManager;
        public AccountController(UserManager<Account> userManager, SignInManager<Account> signInManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var user =await _userManager.Users.ToListAsync();
            return View(user);
        }

        public async Task<IActionResult> AddUser()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddUser(Account user)
        {
            var result = await _userManager.CreateAsync(user,user.Password);
            if (result.Succeeded) {
                return RedirectToAction("Index");
            }
            return View();
        }
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(ViewModels.Login login, string returnUrl=null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {
                var user= await _userManager.FindByNameAsync(login.Username);

                if (user == null)
                {
                    return BadRequest();
                }
                else
                {
                    var result = await _signInManager.PasswordSignInAsync(user, login.Password,true,false);
                    if (result.Succeeded)
                    {
                        return LocalRedirect(returnUrl);
                    }
                        
                    
                }
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Signout()
        {
            _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
}
