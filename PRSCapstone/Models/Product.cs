﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRSCapstone.Models {
    public class Product {

        public int Id { get; set; }
        [Required] [StringLength(30)] public string PartNbr { get; set; }
        [Required] [StringLength(30)] public string Name { get; set; }
        [Column("Price", TypeName = "decimal(11,2)")] public decimal Price { get; set; }
        [Required] [StringLength(30)] public string Unit { get; set; }
        [StringLength(255)] public string PhotoPath { get; set; }
        [Required] public int VendorId { get; set; }

        public virtual Vendor Vendor { get; set; }

        public Product() { }
    }
}
