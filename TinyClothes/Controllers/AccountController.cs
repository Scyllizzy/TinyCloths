using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TinyClothes.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet] // Remember, that is optional. HttpGet is the default
        public IActionResult Register()
        {
            return View();
        }
    }
}