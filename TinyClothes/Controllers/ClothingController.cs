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
            ViewData["CurrentPage"] = pageNumber;

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
                return RedirectToAction(nameof(InventoryList));
            }
            else
            {
                //Return same view with validation messages
                return View(c);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int ID)
        {
            
            Clothing c = await ClothingDB.GetClothingByID(ID, _context);

            if (c == null) // Clothing not in DB
            {
                // returns a HTTP 404 error - Not Found
                return NotFound();
            }

            return View(c);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Clothing c)
        {
            if(ModelState.IsValid)
            {
                await ClothingDB.Edit(c, _context);
                ViewData["Message"] = $"{c.Title} updated successfully";
                return View(c);
            }
            return View(c);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int ID)
        {
            Clothing c = await ClothingDB.GetClothingByID(ID, _context);

            if (c == null)
            {
                // returns a HTTP 404 error - Not Found
                return NotFound();
            }

            return View(c);
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int ID)
        {
            Clothing c = await ClothingDB.GetClothingByID(ID, _context);

            await ClothingDB.Delete(c, _context);
            
            TempData["Message"] = $"{c.Title} deleted successfully";

            return RedirectToAction(nameof(InventoryList));
        }

        public async Task<IActionResult> Search(SearchCriteria search)
        {
            return View();
        }
    }
}