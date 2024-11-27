namespace Test.APIs.Dtos;

public class TeachersWhereInput
{
    public List<string>? ClassesItems { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? FirstName { get; set; }

    public string? Id { get; set; }

    public string? LastName { get; set; }

    public string? Subject { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
