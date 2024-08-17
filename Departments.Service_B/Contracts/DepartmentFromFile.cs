namespace Departments.Service_B.Contracts
{
    public record DepartmentFromFile
    {
        public string Name { get; set; } = null!;

        public string? ParentName { get; set; }
    }
}
