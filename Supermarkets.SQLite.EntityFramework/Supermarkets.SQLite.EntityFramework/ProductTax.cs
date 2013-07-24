using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Supermarkets.SQLite.EntityFramework
{
    [Table("Taxes")]
    public class ProductTax
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        public string ProductName { get; set; }

        [Required]
        public decimal Tax { get; set; }
    }
}