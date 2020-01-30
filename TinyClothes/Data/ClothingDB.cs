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
    }
}
