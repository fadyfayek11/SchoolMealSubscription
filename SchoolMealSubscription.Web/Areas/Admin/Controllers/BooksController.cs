using SchoolMealSubscription.DataAccess.Data;
using SchoolMealSubscription.DataAccess.Repository.IRepository;
using SchoolMealSubscription.Models;
using SchoolMealSubscription.Models.ViewModels;
using SchoolMealSubscription.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using System.Security.Claims;

namespace BookStoreWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]

    public class BooksController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public BooksController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            List<Book> listBooks = _unitOfWork.Book.GetAll(includeProperties:"Category,Club").ToList();
            return View(listBooks);
        }

        public IActionResult Upsert(int? id)
        {
            BookVM bookVM = new()
            {
                CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                Book = new()
            };
            if (id == 0 || id == null)
            {
                //create
                return View(bookVM);

            }
            else
            {
                //update
                bookVM.Book = _unitOfWork.Book.Get(u => u.Id == id);
                return View(bookVM);
            }
        }

        [HttpPost]
        public IActionResult Upsert(BookVM bookVm, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRoot = _webHostEnvironment.WebRootPath; // get wwwRoot folder
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string bookPath = Path.Combine(wwwRoot, @"images\book");
                    if(!string.IsNullOrEmpty(bookVm.Book.ImageUrl))
                    {
                        var oldImagePath = Path.Combine(wwwRoot, bookVm.Book.ImageUrl.TrimStart('\\'));
                        if(System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(bookPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    bookVm.Book.ImageUrl = @"\images\book\" + fileName;
                }

                if (bookVm.Book.Id != 0)
                {
                    _unitOfWork.Book.Update(bookVm.Book);
                    _unitOfWork.Save();
                    TempData["success"] = "Book updated successfully";
                }
                else
                {
                    _unitOfWork.Book.Add(bookVm.Book);
                    _unitOfWork.Save();
                    TempData["success"] = "Book Added successfully";
                }
                return RedirectToAction("Index", "Books");
            }
            else
            {
                bookVm.CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });
                return View(bookVm);
            }
        }

        #region API CAll
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Book> listBooks = _unitOfWork.Book.GetAll(includeProperties: "Category").ToList();
            return Json(new {data = listBooks});
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var objectToDelete = _unitOfWork.Book.Get(u=> u.Id == id);
            if (objectToDelete == null)
            {
                return Json(new {success = false, message = "Delete Failure"});
            }
            var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, objectToDelete.ImageUrl.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }
            _unitOfWork.Book.Remove(objectToDelete);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });
        }
        
        #endregion 
    }
}
