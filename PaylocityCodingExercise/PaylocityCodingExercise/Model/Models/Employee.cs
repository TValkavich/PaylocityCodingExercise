using System.ComponentModel.DataAnnotations;

namespace PaylocityCodingExercise.Model.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        public int SalaryRaw { get; set; }
        [Required]
        [MaxLength(100)]
        public string BenefitsPackage { get; set; }
    }
}
