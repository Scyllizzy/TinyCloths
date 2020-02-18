﻿using System;
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

        public AccountController(StoreContext context)
        {
            _context = context;
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
                    // Create user session
                    HttpContext.Session.SetInt32("ID", acc.AccountID);
                    HttpContext.Session.SetString("Username", acc.Username);
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
                bool isMatch = await AccountDB.DoesUserMatch(log, _context);
                // TODO: Create session
                return RedirectToAction("Index", "Home");
            }
            return View(log);
        }
    }
}