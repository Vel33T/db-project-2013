using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

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