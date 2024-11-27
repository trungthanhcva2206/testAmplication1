using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Test.Core.Enums;

namespace Test.Infrastructure.Models;

[Table("Students")]
public class StudentsDbModel
{
    [Required()]
    public DateTime CreatedAt { get; set; }

    public string? Email { get; set; }

    public DateTime? EnrollmentDate { get; set; }

    public List<EnrollmentsDbModel>? EnrollmentsItems { get; set; } =
        new List<EnrollmentsDbModel>();

    [StringLength(1000)]
    public string? FirstName { get; set; }

    public GradeLevelEnum? GradeLevel { get; set; }

    [Key()]
    [Required()]
    public string Id { get; set; }

    [StringLength(1000)]
    public string? LastName { get; set; }

    [Required()]
    public DateTime UpdatedAt { get; set; }
}
