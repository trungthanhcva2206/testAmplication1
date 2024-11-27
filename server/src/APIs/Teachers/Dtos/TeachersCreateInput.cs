namespace Test.APIs.Dtos;

public class TeachersCreateInput
{
    public List<Classes>? ClassesItems { get; set; }

    public DateTime CreatedAt { get; set; }

    public string? FirstName { get; set; }

    public string? Id { get; set; }

    public string? LastName { get; set; }

    public Subjects? Subject { get; set; }

    public DateTime UpdatedAt { get; set; }
}
