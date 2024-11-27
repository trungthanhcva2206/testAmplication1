using Test.Core.Enums;

namespace Test.APIs.Dtos;

public class StudentsUpdateInput
{
    public DateTime? CreatedAt { get; set; }

    public string? Email { get; set; }

    public DateTime? EnrollmentDate { get; set; }

    public List<string>? EnrollmentsItems { get; set; }

    public string? FirstName { get; set; }

    public GradeLevelEnum? GradeLevel { get; set; }

    public string? Id { get; set; }

    public string? LastName { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
