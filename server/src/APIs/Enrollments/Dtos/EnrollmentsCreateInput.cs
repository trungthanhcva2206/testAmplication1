namespace Test.APIs.Dtos;

public class EnrollmentsCreateInput
{
    public Classes? ClassField { get; set; }

    public DateTime CreatedAt { get; set; }

    public string? Id { get; set; }

    public Students? Student { get; set; }

    public DateTime UpdatedAt { get; set; }
}
