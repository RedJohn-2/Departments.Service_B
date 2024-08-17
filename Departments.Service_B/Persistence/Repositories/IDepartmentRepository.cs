using Departments.Service_B.Contracts;
using Departments.Service_B.Models;
using Departments.Service_B.Persistence.Entities;

namespace Departments.Service_B.Persistence.Repositories
{
    public interface IDepartmentRepository
    {
        Task<IReadOnlyList<Department>> GetAll();

        Task Synchronize(IEnumerable<DepartmentFromFile> departments);
    }
}
