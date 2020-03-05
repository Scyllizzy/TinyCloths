using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TinyClothes.Models
{
    public class SearchCriteria
    {
        private const string MinPriceName = "Min Price";
        private const string MaxPriceName = "Max Price";

        public SearchCriteria()
        {
            Results = new List<Clothing>();
        }

        public string Size { get; set; }

        /// <summary>
        /// Type of clothing (shirt, pants, hat, etc...)
        /// </summary>
        public string Type { get; set; }

        [StringLength(150)]
        public string Title { get; set; }

        [Display(Name = MinPriceName)]
        [Range(0.0, double.MaxValue, ErrorMessage = "Min value must be positive")]
        public double? MinPrice { get; set; }

        [Display(Name = MaxPriceName)]
        [Range(0.0, double.MaxValue, ErrorMessage = "Max value must be positive")]
        public double? MaxPrice { get; set; }

        public List<Clothing> Results { get; set; }
    }
}
