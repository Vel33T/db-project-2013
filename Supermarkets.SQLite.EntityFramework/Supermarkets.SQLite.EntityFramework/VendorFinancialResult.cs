
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
    public class VendorFinancialResult
    {
        [Key]
        [Required]
        public string VendorName { get; set; }

        [Required]
        public decimal Incomes { get; set; }

        [Required]
        public decimal Expanses { get; set; }

        [Required]
        public decimal Taxes { get; set; }

        [Required]
        public decimal FinancialResult { get; set; }
    }
}
