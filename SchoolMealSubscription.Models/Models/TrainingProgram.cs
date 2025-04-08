using System.ComponentModel.DataAnnotations.Schema;

namespace Guidxus.Models;

public class TrainingProgram
{
    public int Id { get; set; }              // Primary Key
    public string Name { get; set; }   // Name of the training program
    public string Description { get; set; }   // Description of the program
    public int DurationInWeeks { get; set; }  // Duration in weeks or another unit

    // Foreign keys and navigation properties
    [ForeignKey(nameof(Department))]
    public int? DepartmentId { get; set; }
    public Department? Department { get; set; }  // Navigation property to the department

    public ICollection<Specialization>? Specializations { get; set; }

    // One-to-many relationship with courses
    public ICollection<Course>? Courses { get; set; } = new List<Course>();

    // One-to-many relationship with trainees
    public ICollection<Trainee>? Trainees { get; set; } = new List<Trainee>();
}