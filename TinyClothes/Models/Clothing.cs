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
        [Required(ErrorMessage = "Size is required")]
        public string Size { get; set; }

        /// <summary>
        /// Type of clothing (shirt, pants, etc...)
        /// </summary>
        [Required]
        public string Type { get; set; }
         
        /// <summary>
        /// Color of the clothing
        /// </summary>
        [Required]
        public string Color { get; set; }

        /// <summary>
        /// Retail price of the clothing
        /// </summary>
        [Range(0.01, 300.0)]
        public double Price{ get; set; }

        /// <summary>
        /// A display title of the clothing
        /// </summary>
        [Required]
        [StringLength(35)]
        // Sample Regex, great for validation
        //[RegularExpression("^([A-Za-z0-9])+$")]
        public string Title { get; set; }

        /// <summary>
        /// A brief description of the clothing
        /// </summary>
        [StringLength(800)]
        public string Description { get; set; }
    }
}
