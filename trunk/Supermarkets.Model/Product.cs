using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Supermarkets.Model
{
    public class Product
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MinLength(1)]
        public string ProductName { get; set; }

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
