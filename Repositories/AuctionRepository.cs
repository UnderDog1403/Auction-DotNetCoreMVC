using MyApp.Models;
using MyApp.Data;
namespace MyApp.Repositories
{
    public class AuctionRepository : IAuctionRepository
    {
        private readonly ApplicationDbContext _context;

        public AuctionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Auction> GetAll()
        {
            return _context.Auctions.ToList();
        }
        public IEnumerable<Auction> GetApprovedAuctions()
        {
            return _context.Auctions.Where(a => a.Status == AuctionStatus.Approved).ToList();
        }
        public IEnumerable<Auction> GetActiveAuctions()
        {
            var now = DateTime.Now;
            return _context.Auctions.Where(a => a.Status == AuctionStatus.Approved && a.TimeStart <= now && a.TimeEnd >= now).ToList();
        }
        public Auction GetById(int id)
        {
            return _context.Auctions.Find(id);
        }

        public void Add(Auction auction)
        {
            _context.Auctions.Add(auction);
        }
        public void Update(Auction auction)
        {
            _context.Auctions.Update(auction);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}