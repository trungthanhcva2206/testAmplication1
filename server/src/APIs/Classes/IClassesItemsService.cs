using Test.APIs.Common;
using Test.APIs.Dtos;

namespace Test.APIs;

public interface IClassesItemsService
{
    /// <summary>
    /// Create one Classes
    /// </summary>
    public Task<Classes> CreateClasses(ClassesCreateInput classes);

    /// <summary>
    /// Delete one Classes
    /// </summary>
    public Task DeleteClasses(ClassesWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many ClassesItems
    /// </summary>
    public Task<List<Classes>> ClassesItems(ClassesFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Classes records
    /// </summary>
    public Task<MetadataDto> ClassesItemsMeta(ClassesFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Classes
    /// </summary>
    public Task<Classes> Classes(ClassesWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Classes
    /// </summary>
    public Task UpdateClasses(ClassesWhereUniqueInput uniqueId, ClassesUpdateInput updateDto);

    /// <summary>
    /// Connect multiple EnrollmentsItems records to Classes
    /// </summary>
    public Task ConnectEnrollmentsItems(
        ClassesWhereUniqueInput uniqueId,
        EnrollmentsWhereUniqueInput[] enrollmentsId
    );

    /// <summary>
    /// Disconnect multiple EnrollmentsItems records from Classes
    /// </summary>
    public Task DisconnectEnrollmentsItems(
        ClassesWhereUniqueInput uniqueId,
        EnrollmentsWhereUniqueInput[] enrollmentsId
    );

    /// <summary>
    /// Find multiple EnrollmentsItems records for Classes
    /// </summary>
    public Task<List<Enrollments>> FindEnrollmentsItems(
        ClassesWhereUniqueInput uniqueId,
        EnrollmentsFindManyArgs EnrollmentsFindManyArgs
    );

    /// <summary>
    /// Update multiple EnrollmentsItems records for Classes
    /// </summary>
    public Task UpdateEnrollmentsItems(
        ClassesWhereUniqueInput uniqueId,
        EnrollmentsWhereUniqueInput[] enrollmentsId
    );

    /// <summary>
    /// Get a teacher record for Classes
    /// </summary>
    public Task<Teachers> GetTeacher(ClassesWhereUniqueInput uniqueId);
}
