using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TinyClothes.Models
{
    /// <summary>
    /// Helper class to manage users shopping cart data using cookies
    /// </summary>
    public static class CartHelper
    {
        private const string CartCookie = "CartCookie";

        public static void Add(Clothing c, IHttpContextAccessor http)
        {
            List<Clothing> clothes = GetAllClothes(http);
            clothes.Add(c);

            string data = JsonConvert.SerializeObject(clothes);

            CookieOptions options = new CookieOptions()
            {
                Expires = DateTime.Now.AddDays(14),
                IsEssential = true,
                Secure = true
            };

            http.HttpContext.Response.Cookies.Append(CartCookie, data, options);
        }

        public static int GetItemCount(IHttpContextAccessor http)
        {
            List<Clothing> allClothes = GetAllClothes(http);
            return allClothes.Count;
        }

        /// <summary>
        /// Returns all clothing currently stored in the users cookie.
        /// If no items are present, and empty list is returned.
        /// </summary>
        public static List<Clothing> GetAllClothes(IHttpContextAccessor http)
        {
            string data = http.HttpContext.Request.Cookies[CartCookie];

            if (string.IsNullOrWhiteSpace(data))
            {
                return new List<Clothing>();
            }

            return JsonConvert.DeserializeObject<List<Clothing>>(data);
        }
    }
}
