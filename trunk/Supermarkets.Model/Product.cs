using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Supermarkets.Model
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [MinLength(1)]
        public string Name { get; set; }

        [Required]
        public int MeasureId { get; set; }

        public virtual Measure Measure { get; set; }

        [Required]
        public int VendorId { get; set; }

        public virtual Vendor Vendor { get; set; }

        [Required]
        public decimal BasePrice { get; set; }
    }
}