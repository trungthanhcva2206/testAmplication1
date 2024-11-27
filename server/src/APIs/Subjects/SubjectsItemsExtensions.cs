using Test.APIs.Dtos;
using Test.Infrastructure.Models;

namespace Test.APIs.Extensions;

public static class SubjectsItemsExtensions
{
    public static Subjects ToDto(this SubjectsDbModel model)
    {
        return new Subjects
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            SubjectName = model.SubjectName,
            TeachersItems = model.TeachersItems?.Select(x => x.Id).ToList(),
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static SubjectsDbModel ToModel(
        this SubjectsUpdateInput updateDto,
        SubjectsWhereUniqueInput uniqueId
    )
    {
        var subjects = new SubjectsDbModel
        {
            Id = uniqueId.Id,
            SubjectName = updateDto.SubjectName
        };

        if (updateDto.CreatedAt != null)
        {
            subjects.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            subjects.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return subjects;
    }
}
