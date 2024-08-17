namespace Departments.Service_B.Models
{
    public class Department
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;

        public Guid? ParentId {  get; set; }

        public Department? Parent;

        public List<Department> Children { get; set; } = new();
    }
}
