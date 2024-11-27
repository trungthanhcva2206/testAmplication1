using Test.APIs.Common;
using Test.APIs.Dtos;

namespace Test.APIs;

public interface ITeachersItemsService
{
    /// <summary>
    /// Create one Teachers
    /// </summary>
    public Task<Teachers> CreateTeachers(TeachersCreateInput teachers);

    /// <summary>
    /// Delete one Teachers
    /// </summary>
    public Task DeleteTeachers(TeachersWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many TeachersItems
    /// </summary>
    public Task<List<Teachers>> TeachersItems(TeachersFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Teachers records
    /// </summary>
    public Task<MetadataDto> TeachersItemsMeta(TeachersFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Teachers
    /// </summary>
    public Task<Teachers> Teachers(TeachersWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Teachers
    /// </summary>
    public Task UpdateTeachers(TeachersWhereUniqueInput uniqueId, TeachersUpdateInput updateDto);

    /// <summary>
    /// Connect multiple ClassesItems records to Teachers
    /// </summary>
    public Task ConnectClassesItems(
        TeachersWhereUniqueInput uniqueId,
        ClassesWhereUniqueInput[] classesId
    );

    /// <summary>
    /// Disconnect multiple ClassesItems records from Teachers
    /// </summary>
    public Task DisconnectClassesItems(
        TeachersWhereUniqueInput uniqueId,
        ClassesWhereUniqueInput[] classesId
    );

    /// <summary>
    /// Find multiple ClassesItems records for Teachers
    /// </summary>
    public Task<List<Classes>> FindClassesItems(
        TeachersWhereUniqueInput uniqueId,
        ClassesFindManyArgs ClassesFindManyArgs
    );

    /// <summary>
    /// Update multiple ClassesItems records for Teachers
    /// </summary>
    public Task UpdateClassesItems(
        TeachersWhereUniqueInput uniqueId,
        ClassesWhereUniqueInput[] classesId
    );

    /// <summary>
    /// Get a subject record for Teachers
    /// </summary>
    public Task<Subjects> GetSubject(TeachersWhereUniqueInput uniqueId);
}
