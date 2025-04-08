using SchoolMealSubscription.DataAccess.Repository.IRepository;
using SchoolMealSubscription.Models;
using SchoolMealSubscription.Utility;
using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Spreadsheet;
using GemBox.Document;
using GemBox.Document.Tables;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using Table = GemBox.Document.Tables.Table;

namespace BookStoreWeb.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = SD.Role_Admin)]

public class UsersController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    public UsersController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public IActionResult Index()
    {
        List<ApplicationUser> users = _unitOfWork.ApplicationUser.GetAll(x=>!x.IsAdmin).ToList();
        return View(users);
    }



    public IActionResult Edit(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            return NotFound();
        }
        ApplicationUser author = _unitOfWork.ApplicationUser.Get(c => c.Id == id);
        if (author == null)
        {
            return NotFound();
        }
        return View(author);
    }

    [HttpPost]
    public IActionResult Edit(ApplicationUser user)
    {
        var userDb = _unitOfWork.ApplicationUser.Get(x=>x.Id == user.Id);
        if (userDb.Email == null)
        {
            return NotFound();
        }
        userDb.Email = user.Email;
        userDb.PhoneNumber = user.PhoneNumber;
 
        _unitOfWork.ApplicationUser.Update(userDb);
        _unitOfWork.Save();
        TempData["success"] = "User edited successfully";
        return RedirectToAction("Index", "Users");
    }

    public IActionResult Delete(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            return NotFound();
        }
        ApplicationUser user = _unitOfWork.ApplicationUser.Get(c => c.Id == id);
        if (user == null)
        {
            return NotFound();
        }
        return View(user);
    }

    [HttpPost, ActionName("Delete")]
    public IActionResult DeletePost(string id)
    {
        ApplicationUser? user = _unitOfWork.ApplicationUser.Get(c => c.Id == id);

        if (user == null)
        {
            return NotFound();
        }
        _unitOfWork.ApplicationUser.Remove(user);
        _unitOfWork.Save();
        TempData["success"] = "User deleted successfully";
        return RedirectToAction("Index", "Users");
    }

    public IActionResult ExportToExcel()
    {
        // Use EPPlus to create an Excel file
        using (var package = new ExcelPackage())
        {
            var worksheet = package.Workbook.Worksheets.Add("Users");
            worksheet.Cells[1, 1].Value = "Phone Number";
            worksheet.Cells[1, 2].Value = "Email";

            var users = _unitOfWork.ApplicationUser.GetAll(x=>!x.IsAdmin).ToList();
            for (int i = 0; i < users.Count; i++)
            {
                worksheet.Cells[i + 2, 1].Value = users[i].PhoneNumber;
                worksheet.Cells[i + 2, 2].Value = users[i].Email;
            }

            // Autofit columns to fill the data
            worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

            var stream = new MemoryStream();
            package.SaveAs(stream);
            stream.Position = 0;
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "User.xlsx");
        }
    }



    public IActionResult ExportToPdf()
    {
        // Create a new document
        var document = new DocumentModel();

        // Create a section for the title and table (title and table will be in the same section)
        var section = new Section(document);

        // Add title to the section
        var title = new Paragraph(document, "Users Report")
        {
            ParagraphFormat = { Alignment = HorizontalAlignment.Center }
        };
        section.Blocks.Add(title);

        // Create a table with headers
        var table = new Table(document);
        var headerRow = new TableRow(document);
        headerRow.Cells.Add(new TableCell(document, "Phone Number"));
        headerRow.Cells.Add(new TableCell(document, "Email"));
        table.Rows.Add(headerRow);

        // Add author data to the table
        var users = _unitOfWork.ApplicationUser.GetAll(x=>!x.IsAdmin).ToList();
        foreach (var user in users)
        {
            var row = new TableRow(document);
            row.Cells.Add(new TableCell(document, user.PhoneNumber));
            row.Cells.Add(new TableCell(document, user.Email));
            table.Rows.Add(row);
        }

        // Add the table to the section
        section.Blocks.Add(table);

        // Add the section to the document
        document.Sections.Add(section);

        // Save as PDF to memory stream
        using (var stream = new MemoryStream())
        {
            document.Save(stream, SaveOptions.PdfDefault);
            stream.Position = 0;
            return File(stream.ToArray(), "application/pdf", "Users.pdf");
        }
    }


    public IActionResult ExportToWord()
    {
        // Create a new document
        var document = new DocumentModel();

        // Add title (ensure it's in the same section as the table)
        var title = new Paragraph(document, "Users Report")
        {
            ParagraphFormat = { Alignment = HorizontalAlignment.Center }
        };

        // Create a section and add the title
        var section = new Section(document);
        section.Blocks.Add(title);

        // Create a table with headers
        var table = new Table(document);

        // Add header row for the table
        var headerRow = new TableRow(document);
        headerRow.Cells.Add(new TableCell(document, "Phone Number"));
        headerRow.Cells.Add(new TableCell(document, "Email"));
        table.Rows.Add(headerRow);

        // Add author data to the table
        var users = _unitOfWork.ApplicationUser.GetAll(x=>!x.IsAdmin).ToList();
        foreach (var user in users)
        {
            var row = new TableRow(document);
            row.Cells.Add(new TableCell(document, user.PhoneNumber));
            row.Cells.Add(new TableCell(document, user.Email));
            table.Rows.Add(row);
        }

        // Add the table to the same section as the title
        section.Blocks.Add(table);

        // Add section to document
        document.Sections.Add(section);

        // Save the document to a memory stream
        using (var stream = new MemoryStream())
        {
            document.Save(stream, SaveOptions.DocxDefault);
            stream.Position = 0;
            return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "Users.docx");
        }
    }


}