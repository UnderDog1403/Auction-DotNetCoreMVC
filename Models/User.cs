using System.ComponentModel.DataAnnotations;

namespace MyApp.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required, StringLength(200)]
        public string PasswordHash { get; set; } = string.Empty;

        [Required, EmailAddress, StringLength(200)]
        public string Email { get; set; } = string.Empty;

        [StringLength(255)]
        public string? Address { get; set; }

        [Phone]
        public string? Phone { get; set; }

        public int Role { get; set; } = 2;

        public bool IsActivated { get; set; } = true;
        public virtual ICollection<Bid> Bids { get; set; } = new List<Bid>();
        public ICollection<Payment> Payment { get; set; } = new List<Payment>();
         public virtual ICollection<Auction> Auctions { get; set; } = new List<Auction>();


    }
}
