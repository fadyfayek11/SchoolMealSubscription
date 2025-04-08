using System.ComponentModel.DataAnnotations.Schema;

namespace Guidxus.Models;

public class Skill
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    [ForeignKey(nameof(VerifiedMentor))]
    public string? VerifiedMentorId { get; set; }
    public Mentor? VerifiedMentor { get; set; }
}