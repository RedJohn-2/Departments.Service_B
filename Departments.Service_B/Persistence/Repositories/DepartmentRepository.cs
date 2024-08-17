using Departments.Service_B.Models;
using Departments.Service_B.Persistence;
using Departments.Service_B.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

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
            var departments = await _dbContext.Departments.ToListAsync();
            return BuildTree(departments, null);

        }

        private List<Department> BuildTree(List<DepartmentEntity> departments, Guid? parentId)
        {
            return departments
                .Where(d => d.ParentId == parentId)
                .Select(d => new Department
                {
                    Id = d.Id,
                    Name = d.Name,
                    ParentId = d.ParentId,
                    Children = BuildTree(departments, d.Id)
                }).ToList();
        }

        public Task Synchronize(IEnumerable<Department> departments)
        {
            throw new NotImplementedException();
        }
    }
}
