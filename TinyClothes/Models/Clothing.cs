using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TinyClothes.Models
{
    /// <summary>
    /// Represents a single clothing item.
    /// </summary>
    public class Clothing
    {
        /// <summary>
        /// Unique id for the clothing item
        /// </summary>
        [Key] // Set as Primary Key
        public int ItemID { get; set; }

        /// <summary>
        /// Size of the clothing
        /// </summary>
        public string Size { get; set; }

        /// <summary>
        /// Type of clothing (shirt, pants, etc...)
        /// </summary>
        public string Type { get; set; }
         
        /// <summary>
        /// Color of the clothing
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// Retail price of the clothing
        /// </summary>
        public double Price{ get; set; }

        /// <summary>
        /// A display title of the clothing
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// A brief description of the clothing
        /// </summary>
        public string Description { get; set; }
    }
}
