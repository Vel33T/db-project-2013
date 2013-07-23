using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarkets.SQLite.EntityFramework
{
    [Table("VendorFinancialResults")]
    class VendorFinancialResult
    {
        [Key]
        [Required]
        public string VendorName { get; set; }

        [Required]
        public decimal Income { get; set; }

        [Required]
        public decimal Expenses { get; set; }

        [Required]
        public decimal Tax { get; set; }

        [Required]
        public decimal FinancialResult { get; set; }
    }
}
