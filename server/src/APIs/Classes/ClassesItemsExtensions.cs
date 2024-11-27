using Test.APIs.Dtos;
using Test.Infrastructure.Models;

namespace Test.APIs.Extensions;

public static class ClassesItemsExtensions
{
    public static Classes ToDto(this ClassesDbModel model)
    {
        return new Classes
        {
            ClassName = model.ClassName,
            CreatedAt = model.CreatedAt,
            EnrollmentsItems = model.EnrollmentsItems?.Select(x => x.Id).ToList(),
            Id = model.Id,
            Teacher = model.TeacherId,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static ClassesDbModel ToModel(
        this ClassesUpdateInput updateDto,
        ClassesWhereUniqueInput uniqueId
    )
    {
        var classes = new ClassesDbModel { Id = uniqueId.Id, ClassName = updateDto.ClassName };

        if (updateDto.CreatedAt != null)
        {
            classes.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.Teacher != null)
        {
            classes.TeacherId = updateDto.Teacher;
        }
        if (updateDto.UpdatedAt != null)
        {
            classes.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return classes;
    }
}
