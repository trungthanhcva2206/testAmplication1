using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test.Infrastructure.Models;

[Table("Teachers")]
public class TeachersDbModel
{
    public List<ClassesDbModel>? ClassesItems { get; set; } = new List<ClassesDbModel>();

    [Required()]
    public DateTime CreatedAt { get; set; }

    [StringLength(1000)]
    public string? FirstName { get; set; }

    [Key()]
    [Required()]
    public string Id { get; set; }

    [StringLength(1000)]
    public string? LastName { get; set; }

    public string? SubjectId { get; set; }

    [ForeignKey(nameof(SubjectId))]
    public SubjectsDbModel? Subject { get; set; } = null;

    [Required()]
    public DateTime UpdatedAt { get; set; }
}
