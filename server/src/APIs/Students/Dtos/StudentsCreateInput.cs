using Test.Core.Enums;

namespace Test.APIs.Dtos;

public class StudentsCreateInput
{
    public DateTime CreatedAt { get; set; }

    public string? Email { get; set; }

    public DateTime? EnrollmentDate { get; set; }

    public List<Enrollments>? EnrollmentsItems { get; set; }

    public string? FirstName { get; set; }

    public GradeLevelEnum? GradeLevel { get; set; }

    public string? Id { get; set; }

    public string? LastName { get; set; }

    public DateTime UpdatedAt { get; set; }
}
