using System.ComponentModel.DataAnnotations.Schema;

namespace Guidxus.Models;

public class Specialization
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }

    [ForeignKey(nameof(Program))]
    public int? ProgramId { get; set; }
    public TrainingProgram? Program { get; set; }
}