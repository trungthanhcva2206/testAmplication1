using Test.APIs.Common;
using Test.APIs.Dtos;

namespace Test.APIs;

public interface IStudentsItemsService
{
    /// <summary>
    /// Create one Students
    /// </summary>
    public Task<Students> CreateStudents(StudentsCreateInput students);

    /// <summary>
    /// Delete one Students
    /// </summary>
    public Task DeleteStudents(StudentsWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many StudentsItems
    /// </summary>
    public Task<List<Students>> StudentsItems(StudentsFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Students records
    /// </summary>
    public Task<MetadataDto> StudentsItemsMeta(StudentsFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Students
    /// </summary>
    public Task<Students> Students(StudentsWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Students
    /// </summary>
    public Task UpdateStudents(StudentsWhereUniqueInput uniqueId, StudentsUpdateInput updateDto);

    /// <summary>
    /// Connect multiple EnrollmentsItems records to Students
    /// </summary>
    public Task ConnectEnrollmentsItems(
        StudentsWhereUniqueInput uniqueId,
        EnrollmentsWhereUniqueInput[] enrollmentsId
    );

    /// <summary>
    /// Disconnect multiple EnrollmentsItems records from Students
    /// </summary>
    public Task DisconnectEnrollmentsItems(
        StudentsWhereUniqueInput uniqueId,
        EnrollmentsWhereUniqueInput[] enrollmentsId
    );

    /// <summary>
    /// Find multiple EnrollmentsItems records for Students
    /// </summary>
    public Task<List<Enrollments>> FindEnrollmentsItems(
        StudentsWhereUniqueInput uniqueId,
        EnrollmentsFindManyArgs EnrollmentsFindManyArgs
    );

    /// <summary>
    /// Update multiple EnrollmentsItems records for Students
    /// </summary>
    public Task UpdateEnrollmentsItems(
        StudentsWhereUniqueInput uniqueId,
        EnrollmentsWhereUniqueInput[] enrollmentsId
    );
}
