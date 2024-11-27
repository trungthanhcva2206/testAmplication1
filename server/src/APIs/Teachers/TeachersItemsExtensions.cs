using Test.APIs.Dtos;
using Test.Infrastructure.Models;

namespace Test.APIs.Extensions;

public static class TeachersItemsExtensions
{
    public static Teachers ToDto(this TeachersDbModel model)
    {
        return new Teachers
        {
            ClassesItems = model.ClassesItems?.Select(x => x.Id).ToList(),
            CreatedAt = model.CreatedAt,
            FirstName = model.FirstName,
            Id = model.Id,
            LastName = model.LastName,
            Subject = model.SubjectId,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static TeachersDbModel ToModel(
        this TeachersUpdateInput updateDto,
        TeachersWhereUniqueInput uniqueId
    )
    {
        var teachers = new TeachersDbModel
        {
            Id = uniqueId.Id,
            FirstName = updateDto.FirstName,
            LastName = updateDto.LastName
        };

        if (updateDto.CreatedAt != null)
        {
            teachers.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.Subject != null)
        {
            teachers.SubjectId = updateDto.Subject;
        }
        if (updateDto.UpdatedAt != null)
        {
            teachers.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return teachers;
    }
}
