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

        public async Task<IActionResult> InventoryList(int? page)
        {
            const int PageSize = 2;
            // Null coalescing operator
            int pageNumber = page ?? 1;

            ViewData["MaxPage"] = await GetMaxPage(PageSize);

            List<Clothing> clothes = await ClothingDB.GetClothingByPage(_context, pageNum: pageNumber, pageSize: PageSize);
            return View(clothes);
        }

        private async Task<int> GetMaxPage(int PageSize)
        {
            int numProducts = await ClothingDB.GetNumClothing(_context);
            int maxPage = Convert.ToInt32(Math.Ceiling((double)numProducts / PageSize));
            return maxPage;
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