using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TinyClothes.Data;

namespace TinyClothes.Controllers
{
    public class ClothingController : Controller
    {
        private readonly StoreContext _context;

        public ClothingController(StoreContext context)
        {
            _context = context;
        }

        public IActionResult InventoryList()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
    }
}