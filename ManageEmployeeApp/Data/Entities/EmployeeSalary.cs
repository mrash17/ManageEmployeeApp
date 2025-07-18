using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManageEmployeeApp.Data.Entities
{
    public class EmployeeSalary
    {
        [Key]
        public int SalaryId { get; set; }

        [Required]
        public DateTime FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        public double Salary { get; set; }

        [Required]
        public int EmployeeId { get; set; }

        [ForeignKey(nameof(EmployeeId))]
        public Employee Employee { get; set; }
    }
}