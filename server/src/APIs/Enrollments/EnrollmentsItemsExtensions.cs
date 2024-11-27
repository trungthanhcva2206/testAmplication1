using Test.APIs.Dtos;
using Test.Infrastructure.Models;

namespace Test.APIs.Extensions;

public static class EnrollmentsItemsExtensions
{
    public static Enrollments ToDto(this EnrollmentsDbModel model)
    {
        return new Enrollments
        {
            ClassField = model.ClassFieldId,
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            Student = model.StudentId,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static EnrollmentsDbModel ToModel(
        this EnrollmentsUpdateInput updateDto,
        EnrollmentsWhereUniqueInput uniqueId
    )
    {
        var enrollments = new EnrollmentsDbModel { Id = uniqueId.Id };

        if (updateDto.ClassField != null)
        {
            enrollments.ClassFieldId = updateDto.ClassField;
        }
        if (updateDto.CreatedAt != null)
        {
            enrollments.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.Student != null)
        {
            enrollments.StudentId = updateDto.Student;
        }
        if (updateDto.UpdatedAt != null)
        {
            enrollments.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return enrollments;
    }
}
