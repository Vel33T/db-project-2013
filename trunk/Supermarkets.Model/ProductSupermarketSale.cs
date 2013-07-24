using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Supermarkets.Model
{
    public class ProductSupermarketSale
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int ProductId { get; set; }

        public virtual Product Product { get; set; }

        public int SupermarketId { get; set; }

        public virtual Supermarket Supermarket { get; set; }

        [Required]
        public DateTime DateSold { get; set; }

        [Required]
        public decimal UnitPrice { get; set; }

        [Required]
        public decimal Quantity { get; set; }
    }
}