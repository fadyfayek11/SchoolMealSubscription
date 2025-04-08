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
using SchoolMealSubscription.Models.Entities;

namespace SchoolMealSubscription.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = SD.Role_Admin)]

public class UsersController : Controller
{


}