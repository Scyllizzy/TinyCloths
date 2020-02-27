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
            string data = JsonConvert.SerializeObject(c);

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
            string data = http.HttpContext.Request.Cookies[CartCookie];

            if (string.IsNullOrWhiteSpace(data))
            {
                return 0;
            }

            return 1;
        }

        public static List<Clothing> GetAllClothes(IHttpContextAccessor http)
        {
            throw new NotImplementedException();
        }
    }
}
