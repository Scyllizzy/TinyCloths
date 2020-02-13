using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinyClothes.Models;

namespace TinyClothes.Data
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {
        }

        // Add a DbSet for each entity that needs to be tracked by the DB
        // https://docs.microsoft.com/en-us/aspnet/core/data/ef-mvc/intro?view=aspnetcore-3.1#create-the-database-context
        public DbSet<Clothing> Clothing { get; set; }
        public DbSet<Account> Account { get; set; }

        // Add-Migration (Put a name here)
        // Update-Database
    }
}
