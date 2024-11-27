using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test.Infrastructure.Models;

[Table("Enrollments")]
public class EnrollmentsDbModel
{
    public string? ClassFieldId { get; set; }

    [ForeignKey(nameof(ClassFieldId))]
    public ClassesDbModel? ClassField { get; set; } = null;

    [Required()]
    public DateTime CreatedAt { get; set; }

    [Key()]
    [Required()]
    public string Id { get; set; }

    public string? StudentId { get; set; }

    [ForeignKey(nameof(StudentId))]
    public StudentsDbModel? Student { get; set; } = null;

    [Required()]
    public DateTime UpdatedAt { get; set; }
}
