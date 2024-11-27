namespace Test.APIs.Dtos;

public class Subjects
{
    public DateTime CreatedAt { get; set; }

    public string Id { get; set; }

    public string? SubjectName { get; set; }

    public List<string>? TeachersItems { get; set; }

    public DateTime UpdatedAt { get; set; }
}
