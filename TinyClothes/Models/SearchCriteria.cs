using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TinyClothes.Models
{
    public class SearchCriteria
    {
        public SearchCriteria()
        {
            Results = new List<Clothing>();
        }

        public string Size { get; set; }

        /// <summary>
        /// Type of clothing (shirt, pants, hat, etc...)
        /// </summary>
        public string Type { get; set; }

        public string Title { get; set; }

        public double? MinPrice { get; set; }

        public double? MaxPrice { get; set; }

        public List<Clothing> Results { get; set; }
    }
}
