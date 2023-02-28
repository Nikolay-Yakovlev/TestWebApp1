using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApplication1.Models
{
    public class MyViewModel
    {
        public Guid? Seed { get; set; }
        public Department? Department { get; set; }
        public Employee? Employee { get; set; }
        public List<Department>? DepartmentList { get; set; }
        public List<Employee>? EmployeeList { get; set; }

    }

}
