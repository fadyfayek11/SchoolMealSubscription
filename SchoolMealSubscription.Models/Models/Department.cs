using System.ComponentModel.DataAnnotations.Schema;

namespace Guidxus.Models;

public class Department
{
    public int Id { get; set; }
    public string Name { get; set; }
    [ForeignKey(nameof(College))]

    public int? CollegeId { get; set; }
    public College? College { get; set; }

    public ICollection<TrainingProgram> Programs { get; set; }
}