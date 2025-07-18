using System.ComponentModel.DataAnnotations;

namespace ManageEmployeeApp.Data.Entities
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(11)]
        public string SSN { get; set; }

        [Required]
        public DateTime DOB { get; set; }

        [MaxLength(200)]
        public string Address { get; set; }

        [MaxLength(50)]
        public string City { get; set; }

        [MaxLength(50)]
        public string State { get; set; }

        [MaxLength(10)]
        public string Zip { get; set; }

        [MaxLength(10)]
        public string Phone { get; set; }

        public DateTime JoinDate { get; set; }

        public DateTime? ExitDate { get; set; }

        public ICollection<EmployeeSalary> Salaries { get; set; } = new List<EmployeeSalary>();
    }
}