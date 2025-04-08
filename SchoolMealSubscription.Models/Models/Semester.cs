using System.ComponentModel.DataAnnotations.Schema;

namespace Guidxus.Models;

public class Semester
{
    public int Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }

    [ForeignKey(nameof(Collage))]
    public int? CollageId { get; set; }
    public College? Collage { get; set; }

    public DateTime StarDate { get; set; }
    public DateTime EndDate { get; set; }
}