using Departments.Service_B.Models;
using Departments.Service_B.Persistence.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Departments.Service_B.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly IDepartmentRepository _departmentRepository;
        public DepartmentsController(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public async Task<IActionResult> Index()
        {
            var departments = await _departmentRepository.GetAll();

            return View(departments);
        }
    }
}
