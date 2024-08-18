using Departments.Service_B.Contracts;
using Departments.Service_B.Models;
using Departments.Service_B.Persistence;
using Departments.Service_B.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using System.Net.WebSockets;

namespace Departments.Service_B.Persistence.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly ApplicationContext _dbContext;

        public DepartmentRepository(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IReadOnlyList<Department>> GetAll()
        {
            var departmentEntities = await _dbContext.Departments.ToListAsync();

            var departments = departmentEntities.Select(d => new Department
            {
                Id = d.Id,
                Name = d.Name,
                ParentId = d.ParentId
            }).ToList();

            var departmentDict = departments.ToDictionary(d => d.Id, d => d);
            FillDepartmentDictionary(departments, departmentDict);
            return departments;

        }

        private void FillDepartmentDictionary(
            List<Department> departments,
            Dictionary<Guid, Department> departmentDict)
        {
            departments
                .Where(d => d.ParentId == null)
                .ToList()
                .ForEach(d =>
                {
                    d.Children = FillChildren(departments, departmentDict, d.Id);
                });
        }

        private List<Department> FillChildren(
            List<Department> departments,
            Dictionary<Guid, Department> departmentDict,
            Guid parentId)
        {
            return departments
                .Where(d => d.ParentId == parentId)
                .Select(d =>
                {
                    d.Parent = departmentDict[parentId];
                    d.Children = FillChildren(departments, departmentDict, d.Id);
                    return d;
                }).ToList();
        }

        public async Task Synchronize(IEnumerable<DepartmentFromFile> departments)
        {
            var existingDepartments = await _dbContext.Departments.ToListAsync();

            var searchingDictionary = existingDepartments.ToDictionary(d => d.Name);

            var newDepartments = new List<DepartmentEntity>();

            foreach (var department in departments)
            {
                if (!searchingDictionary.TryGetValue(department.Name, out var existingDepartment))
                {
                    existingDepartment = new DepartmentEntity
                    {
                        Name = department.Name
                    };

                    newDepartments.Add(existingDepartment);
                    searchingDictionary[existingDepartment.Name] = existingDepartment;
                }

                if (department.ParentName != null)
                {
                    if (searchingDictionary.TryGetValue(department.ParentName,out var existingParent))
                    {
                        existingDepartment.ParentId = existingParent.Id;
                    }
                }
            }

            await _dbContext.AddRangeAsync(newDepartments);
            await _dbContext.SaveChangesAsync();
        }
    }
}
