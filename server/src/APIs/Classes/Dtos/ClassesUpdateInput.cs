namespace Test.APIs.Dtos;

public class ClassesUpdateInput
{
    public string? ClassName { get; set; }

    public DateTime? CreatedAt { get; set; }

    public List<string>? EnrollmentsItems { get; set; }

    public string? Id { get; set; }

    public string? Teacher { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
