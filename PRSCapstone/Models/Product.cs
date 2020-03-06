using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PRSCapstone.Models {
    public class Product {

        public int Id { get; set; }
        [Required] public string Description { get; set; }
        public int VendorId { get; set; }

        public virtual Vendor Vendor { get; set; }
        public Product() { }
    }
}
