using Test.APIs.Common;
using Test.APIs.Dtos;

namespace Test.APIs;

public interface ISubjectsItemsService
{
    /// <summary>
    /// Create one Subjects
    /// </summary>
    public Task<Subjects> CreateSubjects(SubjectsCreateInput subjects);

    /// <summary>
    /// Delete one Subjects
    /// </summary>
    public Task DeleteSubjects(SubjectsWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many SubjectsItems
    /// </summary>
    public Task<List<Subjects>> SubjectsItems(SubjectsFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Subjects records
    /// </summary>
    public Task<MetadataDto> SubjectsItemsMeta(SubjectsFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Subjects
    /// </summary>
    public Task<Subjects> Subjects(SubjectsWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Subjects
    /// </summary>
    public Task UpdateSubjects(SubjectsWhereUniqueInput uniqueId, SubjectsUpdateInput updateDto);

    /// <summary>
    /// Connect multiple TeachersItems records to Subjects
    /// </summary>
    public Task ConnectTeachersItems(
        SubjectsWhereUniqueInput uniqueId,
        TeachersWhereUniqueInput[] teachersId
    );

    /// <summary>
    /// Disconnect multiple TeachersItems records from Subjects
    /// </summary>
    public Task DisconnectTeachersItems(
        SubjectsWhereUniqueInput uniqueId,
        TeachersWhereUniqueInput[] teachersId
    );

    /// <summary>
    /// Find multiple TeachersItems records for Subjects
    /// </summary>
    public Task<List<Teachers>> FindTeachersItems(
        SubjectsWhereUniqueInput uniqueId,
        TeachersFindManyArgs TeachersFindManyArgs
    );

    /// <summary>
    /// Update multiple TeachersItems records for Subjects
    /// </summary>
    public Task UpdateTeachersItems(
        SubjectsWhereUniqueInput uniqueId,
        TeachersWhereUniqueInput[] teachersId
    );
}
