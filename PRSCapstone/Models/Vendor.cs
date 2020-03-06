﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PRSCapstone.Models {
    public class Vendor {

        public int Id { get; set; }
        [StringLength(30)] public string Code { get; set; }
        [StringLength(30)] public string Name { get; set; }
        [StringLength(30)] public string Address { get; set; }
        [StringLength(30)] public string City { get; set; }
        [StringLength(2)]public string State { get; set; }
        [StringLength(5)]public string ZipCode { get; set; }
        [StringLength(12)]public string Phone { get; set; }
        [StringLength(255)]public string Email { get; set; }

        public Vendor() { }
    }
}
