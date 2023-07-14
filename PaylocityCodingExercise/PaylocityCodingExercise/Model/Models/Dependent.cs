using System.ComponentModel.DataAnnotations;

namespace PaylocityCodingExercise.Model.Models
{
    public class Dependent
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int EmployeeId { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
