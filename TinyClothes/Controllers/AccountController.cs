using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TinyClothes.Data;
using TinyClothes.Models;

namespace TinyClothes.Controllers
{
    public class AccountController : Controller
    {
        private readonly StoreContext _context;
        private readonly IHttpContextAccessor _http;

        public AccountController(StoreContext context, IHttpContextAccessor http)
        {
            _context = context;
            _http = http;
        }

        [HttpGet] // Remember, that is optional. HttpGet is the default
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel reg)
        {
            if (ModelState.IsValid)
            {
                // Check username is not taken
                if (!await AccountDB.IsUsernameTaken(reg.Username, _context))
                {
                    Account acc = new Account()
                    {
                        FullName = reg.FullName,
                        Username = reg.Username,
                        Password = reg.Password,
                        Email = reg.Email
                    };

                    await AccountDB.Register(acc, _context);

                    SessionHelper.CreateUserSession(acc.AccountID, acc.Username, _http);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(nameof(Account.Username), "That username is taken");
                }
            }

            return View(reg);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel log)
        {
            if (ModelState.IsValid)
            {
                Account acc = await AccountDB.DoesUserMatch(log, _context);

                
                if (acc != null)
                {
                    SessionHelper.CreateUserSession(acc.AccountID, acc.Username, _http);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid Credentials");
                }
            }
            return View(log);
        }

        public IActionResult Logout()
        {
            SessionHelper.DestroyUserSession(_http);
            return RedirectToAction("Index", "Home");
        }
    } 
}