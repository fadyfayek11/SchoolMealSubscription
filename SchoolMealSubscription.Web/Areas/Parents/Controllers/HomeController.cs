using SchoolMealSubscription.DataAccess.Repository.IRepository;
using SchoolMealSubscription.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System.Xml.Linq;
using Azure;

namespace BookStoreWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Book> Books = _unitOfWork.Book.GetAll(includeProperties: "Category").OrderBy(x=>x.CreatedDate).Take(6);
            return View(Books);
        }
        public IActionResult Store()
        {
            IEnumerable<Book> Books = _unitOfWork.Book.GetAll(includeProperties: "Category");
            return View(Books);
        }

        public IActionResult Details(int bookId)
        {
            var book = _unitOfWork.Book.Get(u => u.Id == bookId, includeProperties: "Category,Club,BookComments.User");

            return View(book);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Details()
        {
            // Get Current User
            var claimIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            
            _unitOfWork.Save();
            return RedirectToAction(nameof(Store), "Home");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult JoinBookClub(int clubId)
        {
            var club = _unitOfWork.Club.Get(x => x.Id == clubId, "Book,Owner");
            return View(club);
        }
        [HttpPost]
        public IActionResult CreateBookClub(string url, int bookId)
        {
            // Assuming the user is authenticated and you can get the user ID or profile.
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); 
            
            var club = new Club()
            {
                ClubUrl = url,
                BookId = bookId,
                OwnerId = userId
            };
            _unitOfWork.Club.Add(club);
            _unitOfWork.Save();
            
            var book = _unitOfWork.Book.Get(x => x.Id == bookId);
            book.ClubId = club.Id;
            _unitOfWork.Book.Update(book);

            _unitOfWork.UserClub.Add(new UserClub()
            {
                ClubId = club.Id,
                UserId = userId
            });
            _unitOfWork.Save();

            return Json(new { success = true, message = "Created Successful" ,clubId = club.Id});
        }
        [Authorize]
        [HttpPost]
        public IActionResult SubmitComment(string userComment, int bookId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            // Create a new comment object
            var newComment = new BookComments
            {
                Comment = userComment,
                UserId = userId,
                BookId = bookId,
                CreatedTime = DateTime.Now
            };

            // Add the comment to the list
            _unitOfWork.Comments.Add(newComment);
            _unitOfWork.Save();
            // Redirect back to the Index action to show the updated list of comments
            return Json(new { success = true, userComment = newComment.Comment, ownerName = User.Identity?.Name , datePosted = newComment.CreatedTime.ToString("MMMM dd, yyyy hh:mm tt") });
        }
    }
}