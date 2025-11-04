
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyApp.Models;
using MyApp.Repositories;

namespace MyApp.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
   private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public IActionResult Index()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            Console.WriteLine("User ID Claim: " + (userIdClaim != null ? userIdClaim.Value : "None"));
            var userRoleClaim = User.FindFirst(ClaimTypes.Role);
            Console.WriteLine("User Role Claim: " + (userRoleClaim != null ? userRoleClaim.Value : "None"));
            var role = int.Parse(userRoleClaim.Value);
            if (role != 1)
            {
                return Forbid();
            }
            var users = _userRepository.GetAll();
            return View(users);
        }
        public IActionResult Details(int id)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            var user = _userRepository.GetById(userIdClaim != null ? int.Parse(userIdClaim.Value) : 0);
            Console.WriteLine("Fetched User: " + (user != null ? user.Name : "None"));
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

   
    }
}