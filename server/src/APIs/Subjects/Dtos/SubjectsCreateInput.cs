namespace Test.APIs.Dtos;

public class SubjectsCreateInput
{
    public DateTime CreatedAt { get; set; }

    public string? Id { get; set; }

    public string? SubjectName { get; set; }

    public List<Teachers>? TeachersItems { get; set; }

    public DateTime UpdatedAt { get; set; }
}
