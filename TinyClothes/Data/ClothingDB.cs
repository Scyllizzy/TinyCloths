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
        public static List<Clothing> GetAllClothing()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Adds a clothing object to the database
        /// Return the object with the ID populated
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static Clothing Add(Clothing c, StoreContext context)
        {
            context.Add(c); // prepares INSERT query
            context.SaveChanges(); // execute INSERT query

            return c;
        }
    }
}
