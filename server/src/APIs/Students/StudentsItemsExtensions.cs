using Test.APIs.Dtos;
using Test.Infrastructure.Models;

namespace Test.APIs.Extensions;

public static class StudentsItemsExtensions
{
    public static Students ToDto(this StudentsDbModel model)
    {
        return new Students
        {
            CreatedAt = model.CreatedAt,
            Email = model.Email,
            EnrollmentDate = model.EnrollmentDate,
            EnrollmentsItems = model.EnrollmentsItems?.Select(x => x.Id).ToList(),
            FirstName = model.FirstName,
            GradeLevel = model.GradeLevel,
            Id = model.Id,
            LastName = model.LastName,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static StudentsDbModel ToModel(
        this StudentsUpdateInput updateDto,
        StudentsWhereUniqueInput uniqueId
    )
    {
        var students = new StudentsDbModel
        {
            Id = uniqueId.Id,
            Email = updateDto.Email,
            EnrollmentDate = updateDto.EnrollmentDate,
            FirstName = updateDto.FirstName,
            GradeLevel = updateDto.GradeLevel,
            LastName = updateDto.LastName
        };

        if (updateDto.CreatedAt != null)
        {
            students.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            students.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return students;
    }
}
