using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TinyClothes.Models
{
    /// <summary>
    /// A single user account
    /// </summary>
    public class Account
    {
        [Key]
        public int AccountID { get; set; }

        /// <summary>
        /// First and last name
        /// </summary>
        [Required]
        [StringLength(60)]
        public string FullName { get; set; }

        [Required]
        [StringLength(20)]
        public string Username { get; set; }

        [Required]
        [StringLength(150)]
        [DataType(DataType.Password)] // <input tpye="password" />
        public string Password { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }

        public string Adddress { get; set; }
    }
    
    public class RegisterViewModel
    {
        [Required]
        [StringLength(60)]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required]
        [StringLength(20)]
        public string Username { get; set; }

        [Required]
        [StringLength(150)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare(nameof(Password))]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string ConfrimPassword { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
