﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SportShop.Models
{
    public class Product 
    {
        public int ProductID { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        
        [RegularExpression(@"\d")]
        [Required]
        public int Rating { get; set; }

        public int? CategoryID { get; set; }
        public int? ManufacturerId { get; set; }
        public Manufacturer Manufacturer { get; set; }
    }
}
