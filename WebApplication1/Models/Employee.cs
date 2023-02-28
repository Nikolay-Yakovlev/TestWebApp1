using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace WebApplication1.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public Guid? DepartmentID { get; set; }
        [Required()]
        [NotNull]
        [MaxLength(50)]
        public string SurName { get; set; }
        [NotNull]
        [Required()]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [MaxLength(50)]
        public string? Patronymic { get; set; }
        [Required()]
        public DateTime DateOfBirth { get; set; }
        [Required()]
        [MaxLength(4)]
        public string? DocSeries { get; set; }
        [Required()]
        [MaxLength(6)]
        public string? DocNumber { get; set; }
        [MaxLength(50)]
        public string? Position { get; set; }

        public int GetAge()
        {
            return (DateTime.Now.Year - DateOfBirth.Year);
        }
    }
}
