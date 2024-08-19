using Departments.Service_B.Contracts;
using Departments.Service_B.Models;
using Departments.Service_B.Persistence.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Departments.Service_B.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly IDepartmentRepository _departmentRepository;

        private readonly IHttpClientFactory _httpClientFactory;
        public DepartmentsController(
            IDepartmentRepository departmentRepository, 
            IHttpClientFactory httpClientFactory)
        {
            _departmentRepository = departmentRepository;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var departments = await _departmentRepository.GetAll();

            return View(departments);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Synchronize(IFormFile file)
        {
          if (file != null && file.Length > 0)
            {
                using (var reader = new StreamReader(file.OpenReadStream()))
                {
                    var json = reader.ReadToEnd();

                    var departments = JsonConvert.DeserializeObject<List<DepartmentFromFile>>(json);

                    if (departments is not null)
                        await _departmentRepository.Synchronize(departments);
                }
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> RefreshStatuses([FromBody]List<Guid> guids)
        {
            var client = _httpClientFactory.CreateClient("Service_A");
            var response = await client.PostAsJsonAsync("Status/GetStatusesByDepartmentIds", guids);
            
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadFromJsonAsync<Dictionary<Guid, string>>();
                return Ok(data);
            }
            else
            {
                return StatusCode((int)response.StatusCode);
            }
        }
    }
}
