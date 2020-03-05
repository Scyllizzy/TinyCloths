using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinyClothes.Models;

namespace TinyClothes.Data
{
    /// <summary>
    /// Contains DB helper methods for <see cref="Models.Clothing"/>
    /// </summary>
    public static class ClothingDB
    {
        /// <summary>
        /// Returns a specific page of clothing items sorted by ItemID in ascending order
        /// </summary>
        /// <param name="context">The DB context</param>
        /// <param name="pageNum">The page</param>
        /// <param name="pageSize">The number of products per page</param>
        public async static Task<List<Clothing>> GetClothingByPage(StoreContext context, int pageNum, int pageSize)
        {
            // If you wanted page 1, we wouldn't skip any rows, so we must offset by 1
            const int PageOffset = 1;
            return await context.Clothing
                                .Skip(pageSize * (pageNum - PageOffset))
                                .Take(pageSize)
                                .OrderBy(c => c.Title)
                                .ToListAsync();
        }

        /// <summary>
        /// Adds a clothing object to the database
        /// Return the object with the ID populated
        /// </summary>
        /// <param name="c"></param>
        public async static Task<Clothing> Add(Clothing c, StoreContext context)
        {
            await context.AddAsync(c); // prepares INSERT query
            await context.SaveChangesAsync(); // execute INSERT query

            return c;
        }

        /// <summary>
        /// Returns the total number of Clothing items
        /// </summary>
        public async static Task<int> GetNumClothing(StoreContext context)
        {
            return await context.Clothing.CountAsync();
        }

        /// <summary>
        /// Returns a single Clothing object by ClothingID or null if there are no matches
        /// </summary>
        /// <param name="ID">The ID of the clothing that is to be returned</param>
        /// <param name="context">DB context</param>
        public static async Task<Clothing> GetClothingByID(int ID, StoreContext context)
        {
            return await (from clothing in context.Clothing
                          where clothing.ItemID == ID
                          select clothing).SingleOrDefaultAsync();
        }

        public static async Task<Clothing> Edit(Clothing c, StoreContext context)
        {
            await context.AddAsync(c);
            context.Entry(c).State = EntityState.Modified;
            await context.SaveChangesAsync();

            return c;
        }

        public static async Task Delete(Clothing c, StoreContext context)
        {
            await context.AddAsync(c);
            context.Entry(c).State = EntityState.Deleted;
            await context.SaveChangesAsync();
        }

        public static async Task<SearchCriteria> Search(SearchCriteria search, StoreContext context)
        {
            // Prepare Query
            IQueryable<Clothing> allClothes = from c in context.Clothing select c;

            if (search.MinPrice.HasValue)
            {
                // Where price > minPrice
                allClothes = from c in allClothes where c.Price >= search.MinPrice select c;
            }

            if (search.MaxPrice.HasValue)
            {
                // Where price < maxPrice
                allClothes = from c in allClothes where c.Price <= search.MaxPrice select c;
            }

            if (!string.IsNullOrWhiteSpace(search.Size))
            {
                allClothes = from c in allClothes where c.Size == search.Size select c;
            }

            if (!string.IsNullOrWhiteSpace(search.Type))
            {
                allClothes = from c in allClothes where c.Type == search.Type select c;
            }

            if (!string.IsNullOrWhiteSpace(search.Title))
            {
                allClothes = from c in allClothes where c.Title.Contains(search.Title) select c;
            }

            search.Results = await allClothes.ToListAsync();
            return search;
        }
    }
}
