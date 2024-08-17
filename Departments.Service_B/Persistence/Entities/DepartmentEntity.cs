using Departments.Service_B.Models;

namespace Departments.Service_B.Persistence.Entities
{
    public class DepartmentEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Name { get; set; } = string.Empty;

        public Guid? ParentId { get; set; }

        public DepartmentEntity? Parent;

        public List<DepartmentEntity> Children { get; set; } = new();
    }
}
