using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TinyClothes.Data;
using TinyClothes.Models;

namespace TinyClothes
{
    public static class AccountDB
    {
        public static async Task<bool> IsUsernameTaken(string username, StoreContext context)
        {
            return await (from a in context.Account where username == a.Username select a).AnyAsync();
        }

        public static async Task<Account> Register(Account acc, StoreContext context)
        {
            await context.Account.AddAsync(acc);
            await context.SaveChangesAsync();

            return acc;
        }

        /// <summary>
        /// Returns the account of the user with the supplied login credentials. Null is returned if there is no match
        /// </summary>
        /// <param name="log"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public static async Task<Account> DoesUserMatch(LoginViewModel log, StoreContext context)
        {
            return await (from user in context.Account
            where (user.Email == log.UsernameOrEmail || user.Username == log.UsernameOrEmail) && user.Password == log.Password
            select user).SingleOrDefaultAsync();
        }
    }
}