using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Department
    {
        public Guid Id { get; set; }
        public Guid? ParentDepartmentID { get; set; }
        [MaxLength(10)]
        public string? Code { get; set; }
        [Required()]
        [MaxLength(50)]
        public string Name { get; set; }
     
    }
}
