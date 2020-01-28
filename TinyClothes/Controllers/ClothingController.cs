using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TinyClothes.Data;
using TinyClothes.Models;

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
            List<Clothing> clothes = new List<Clothing>();
            return View(clothes);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Clothing c)
        {
            if (ModelState.IsValid)
            {
                await ClothingDB.Add(c, _context);
                // Temp data lasts for one redirect
                TempData["Message"] = $"{c.Title} added successfully";
                return RedirectToAction("InventoryList");
            }
            else
            {
                //Return same view with validation messages
                return View(c);
            }
        }
    }
}