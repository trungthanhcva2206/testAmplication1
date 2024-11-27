using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test.Infrastructure.Models;

[Table("Classes")]
public class ClassesDbModel
{
    [StringLength(1000)]
    public string? ClassName { get; set; }

    [Required()]
    public DateTime CreatedAt { get; set; }

    public List<EnrollmentsDbModel>? EnrollmentsItems { get; set; } =
        new List<EnrollmentsDbModel>();

    [Key()]
    [Required()]
    public string Id { get; set; }

    public string? TeacherId { get; set; }

    [ForeignKey(nameof(TeacherId))]
    public TeachersDbModel? Teacher { get; set; } = null;

    [Required()]
    public DateTime UpdatedAt { get; set; }
}
