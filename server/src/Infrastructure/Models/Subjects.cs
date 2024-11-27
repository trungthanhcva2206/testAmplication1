using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test.Infrastructure.Models;

[Table("Subjects")]
public class SubjectsDbModel
{
    [Required()]
    public DateTime CreatedAt { get; set; }

    [Key()]
    [Required()]
    public string Id { get; set; }

    [StringLength(1000)]
    public string? SubjectName { get; set; }

    public List<TeachersDbModel>? TeachersItems { get; set; } = new List<TeachersDbModel>();

    [Required()]
    public DateTime UpdatedAt { get; set; }
}
