using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyApp.Models
{
    public class Payment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserID { get; set; }

        [Required]
        public int AuctionID { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Navigation (nếu cần)
        public virtual Auction Auction { get; set; }
        public virtual User User { get; set; }
    }
}