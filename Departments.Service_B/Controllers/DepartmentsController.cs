using Departments.Service_B.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Departments.Service_B.Controllers
{
    public class DepartmentsController : Controller
    {

        public DepartmentsController()
        {

        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
