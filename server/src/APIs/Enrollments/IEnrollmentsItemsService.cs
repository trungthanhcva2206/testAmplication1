using Test.APIs.Common;
using Test.APIs.Dtos;

namespace Test.APIs;

public interface IEnrollmentsItemsService
{
    /// <summary>
    /// Create one Enrollments
    /// </summary>
    public Task<Enrollments> CreateEnrollments(EnrollmentsCreateInput enrollments);

    /// <summary>
    /// Delete one Enrollments
    /// </summary>
    public Task DeleteEnrollments(EnrollmentsWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many EnrollmentsItems
    /// </summary>
    public Task<List<Enrollments>> EnrollmentsItems(EnrollmentsFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Enrollments records
    /// </summary>
    public Task<MetadataDto> EnrollmentsItemsMeta(EnrollmentsFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Enrollments
    /// </summary>
    public Task<Enrollments> Enrollments(EnrollmentsWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Enrollments
    /// </summary>
    public Task UpdateEnrollments(
        EnrollmentsWhereUniqueInput uniqueId,
        EnrollmentsUpdateInput updateDto
    );

    /// <summary>
    /// Get a class record for Enrollments
    /// </summary>
    public Task<Classes> GetClassField(EnrollmentsWhereUniqueInput uniqueId);

    /// <summary>
    /// Get a student record for Enrollments
    /// </summary>
    public Task<Students> GetStudent(EnrollmentsWhereUniqueInput uniqueId);
}
