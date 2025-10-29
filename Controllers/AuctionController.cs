using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyApp.Models;
using MyApp.Repositories;

namespace MyApp.Controllers
{
    [Authorize]
    public class AuctionController: Controller
    {
        private readonly IAuctionRepository _auctionRepository;
        private readonly IBidRepository _bidRepository;
        public AuctionController(IAuctionRepository auctionRepository, IBidRepository bidRepository)
        {
            _auctionRepository = auctionRepository;
            _bidRepository = bidRepository;
        }

        [HttpGet]
        public IActionResult Create()
        {
            Console.WriteLine("IsAuthenticated: " + User.Identity.IsAuthenticated + " User: " + User.Identity.Name);
            return View();
        }
        public IActionResult Details(int id)
        {
            var auction = _auctionRepository.GetById(id);
            Console.WriteLine("Auction Details: " + auction.Name);
            ViewData["AuctionId"] = auction.Id;
            var highestBid = _bidRepository.GetHighestBidForAuction(id);
            ViewBag.HighestBid = highestBid?.Price ?? auction.PriceStart;
            Console.WriteLine("Highest Bid: " + ViewBag.HighestBid);
            return View(auction);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Auction auction)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized();
            }
            if (auction.TimeEnd <= auction.TimeStart || auction.TimeStart <= DateTime.Now)
            {
                ModelState.AddModelError(string.Empty, "Thời gian không hợp lệ.");
            }
            auction.UserId = int.Parse(userIdClaim.Value);
            auction.Status = AuctionStatus.Pending;

//             if (!ModelState.IsValid)
// {
//     foreach (var entry in ModelState)
//     {
//         foreach (var error in entry.Value.Errors)
//         {
//             Console.WriteLine($"Lỗi tại {entry.Key}: {error.ErrorMessage}");
//         }
//     }
// }

            if (ModelState.IsValid)
            {
                _auctionRepository.Add(auction);
                _auctionRepository.Save();

                return RedirectToAction("Index", "Home");
            }

            return View(auction);
        }


        [HttpGet]
        public IActionResult ApprovedAuctions()
        {
            var approvedAuctions = _auctionRepository.GetApprovedAuctions();
            return View(approvedAuctions);
        }

        public IActionResult ActiveAuctions()
        {
            var activeAuctions = _auctionRepository.GetActiveAuctions();
            return View(activeAuctions);
        }

    }
}