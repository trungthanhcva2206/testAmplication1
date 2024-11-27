namespace Test.APIs.Dtos;

public class ClassesCreateInput
{
    public string? ClassName { get; set; }

    public DateTime CreatedAt { get; set; }

    public List<Enrollments>? EnrollmentsItems { get; set; }

    public string? Id { get; set; }

    public Teachers? Teacher { get; set; }

    public DateTime UpdatedAt { get; set; }
}
