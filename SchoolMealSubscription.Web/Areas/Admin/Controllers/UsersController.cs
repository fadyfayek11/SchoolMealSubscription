using SchoolMealSubscription.Models;
using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Spreadsheet;
using GemBox.Document;
using GemBox.Document.Tables;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using Table = GemBox.Document.Tables.Table;
using SchoolMealSubscription.Models.Entities;
using SchoolMealSubscription.Services;

namespace SchoolMealSubscription.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = Roles.Role_Admin)]

public class UsersController : Microsoft.AspNetCore.Mvc.Controller
{


}