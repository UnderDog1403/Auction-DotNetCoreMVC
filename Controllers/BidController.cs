using Microsoft.AspNetCore.SignalR;
using MyApp.Hubs;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using MyApp.Models;
using MyApp.Repositories;
using Microsoft.AspNetCore.Authorization;


namespace MyApp.Controllers
{
    [Authorize]
    public class BidController : Controller
    {
        private readonly IBidRepository _bidRepository;
        private readonly IAuctionRepository _auctionRepository;
        private readonly IHubContext<AuctionHub> _hubContext;

        public BidController(IBidRepository bidRepository, IAuctionRepository auctionRepository, IHubContext<AuctionHub> hubContext)
        {
            _bidRepository = bidRepository;
            _auctionRepository = auctionRepository;
            _hubContext = hubContext;

        }
        [HttpPost]
        
        public async Task<IActionResult> PlaceBid(int auctionId, decimal bidAmount)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            var auction = _auctionRepository.GetById(auctionId);
            var userId = int.Parse(userIdClaim.Value);
            Console.WriteLine("Auction: " + (auction != null ? auction.PriceStart : "None"));
            if (auction == null || auction.Status != AuctionStatus.Approved)
            {
                return BadRequest("Phiên đấu giá không hợp lệ.");
            }
            var highestBid = _bidRepository.GetHighestBidForAuction(auctionId);
            Console.WriteLine("Highest Bid: " + (highestBid != null ? highestBid.Price.ToString() : auction.PriceStart.ToString()));
            var minBidAmount = highestBid != null ? highestBid.Price + 1 : auction.PriceStart;
            Console.WriteLine("Min Bid Amount: " + minBidAmount);
            Console.WriteLine("Bid Amount: " + bidAmount);
            if (bidAmount < minBidAmount)
            {
                return BadRequest($"Giá đặt phải cao hơn ít nhất 1 đơn vị so với giá hiện tại: {minBidAmount}");
            }
            if (auction.UserId == userId)
            {
                return BadRequest("Bạn không thể đấu giá sản phẩm của chính mình.");
            }

            var bid = new Bid
            {
                AuctionID = auctionId,
                UserID = int.Parse(userIdClaim.Value),
                Price = bidAmount,
                Time = DateTime.Now
            };

            _bidRepository.Add(bid);
            _bidRepository.Save();
            await _hubContext.Clients.All.SendAsync("ReceiveHighestBid", auctionId, bid.Price);
            return RedirectToAction("Details", "Auction", new { id = auctionId });

            // return Ok("Đặt giá thành công.");
        }
        public IActionResult Index()
        {
            var userRoleClaim = User.FindFirst(ClaimTypes.Role);
            Console.WriteLine("User Role Claim: " + (userRoleClaim != null ? userRoleClaim.Value : "None"));
            var role = int.Parse(userRoleClaim.Value);
            if (role != 1)
            {
                return Forbid();
            }
            var bids = _bidRepository.GetAllBilsWithDetails();
            return View(bids);
        }
    }
}