﻿using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace Ecommerce.Web.UI.Models
{
    public class Catalog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int productid { get; set; }
        public string name { get; set; }
        public string category { get; set; }
        public string brand { get; set; }
        public string? description { get; set; }
        public int stock { get; set; }
        public decimal price { get; set; } // UnitPrice_TRY
        public string? image { get; set; }
    }
}