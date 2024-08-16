using Departments.Service_B.Models;

namespace Departments.Service_B.Persistence.Entities
{
    public class DepartmentEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public Guid? ParentId { get; set; }

        public DepartmentEntity? Parent = new();

        public List<DepartmentEntity> Children { get; set; } = new();
    }
}
