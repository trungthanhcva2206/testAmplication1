using Microsoft.EntityFrameworkCore;
using Test.APIs;
using Test.APIs.Common;
using Test.APIs.Dtos;
using Test.APIs.Errors;
using Test.APIs.Extensions;
using Test.Infrastructure;
using Test.Infrastructure.Models;

namespace Test.APIs;

public abstract class SubjectsItemsServiceBase : ISubjectsItemsService
{
    protected readonly TestDbContext _context;

    public SubjectsItemsServiceBase(TestDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Subjects
    /// </summary>
    public async Task<Subjects> CreateSubjects(SubjectsCreateInput createDto)
    {
        var subjects = new SubjectsDbModel
        {
            CreatedAt = createDto.CreatedAt,
            SubjectName = createDto.SubjectName,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            subjects.Id = createDto.Id;
        }
        if (createDto.TeachersItems != null)
        {
            subjects.TeachersItems = await _context
                .TeachersItems.Where(teachers =>
                    createDto.TeachersItems.Select(t => t.Id).Contains(teachers.Id)
                )
                .ToListAsync();
        }

        _context.SubjectsItems.Add(subjects);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<SubjectsDbModel>(subjects.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Subjects
    /// </summary>
    public async Task DeleteSubjects(SubjectsWhereUniqueInput uniqueId)
    {
        var subjects = await _context.SubjectsItems.FindAsync(uniqueId.Id);
        if (subjects == null)
        {
            throw new NotFoundException();
        }

        _context.SubjectsItems.Remove(subjects);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many SubjectsItems
    /// </summary>
    public async Task<List<Subjects>> SubjectsItems(SubjectsFindManyArgs findManyArgs)
    {
        var subjectsItems = await _context
            .SubjectsItems.Include(x => x.TeachersItems)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return subjectsItems.ConvertAll(subjects => subjects.ToDto());
    }

    /// <summary>
    /// Meta data about Subjects records
    /// </summary>
    public async Task<MetadataDto> SubjectsItemsMeta(SubjectsFindManyArgs findManyArgs)
    {
        var count = await _context.SubjectsItems.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Subjects
    /// </summary>
    public async Task<Subjects> Subjects(SubjectsWhereUniqueInput uniqueId)
    {
        var subjectsItems = await this.SubjectsItems(
            new SubjectsFindManyArgs { Where = new SubjectsWhereInput { Id = uniqueId.Id } }
        );
        var subjects = subjectsItems.FirstOrDefault();
        if (subjects == null)
        {
            throw new NotFoundException();
        }

        return subjects;
    }

    /// <summary>
    /// Update one Subjects
    /// </summary>
    public async Task UpdateSubjects(
        SubjectsWhereUniqueInput uniqueId,
        SubjectsUpdateInput updateDto
    )
    {
        var subjects = updateDto.ToModel(uniqueId);

        if (updateDto.TeachersItems != null)
        {
            subjects.TeachersItems = await _context
                .TeachersItems.Where(teachers =>
                    updateDto.TeachersItems.Select(t => t).Contains(teachers.Id)
                )
                .ToListAsync();
        }

        _context.Entry(subjects).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.SubjectsItems.Any(e => e.Id == subjects.Id))
            {
                throw new NotFoundException();
            }
            else
            {
                throw;
            }
        }
    }

    /// <summary>
    /// Connect multiple TeachersItems records to Subjects
    /// </summary>
    public async Task ConnectTeachersItems(
        SubjectsWhereUniqueInput uniqueId,
        TeachersWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .SubjectsItems.Include(x => x.TeachersItems)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .TeachersItems.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();
        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        var childrenToConnect = children.Except(parent.TeachersItems);

        foreach (var child in childrenToConnect)
        {
            parent.TeachersItems.Add(child);
        }

        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Disconnect multiple TeachersItems records from Subjects
    /// </summary>
    public async Task DisconnectTeachersItems(
        SubjectsWhereUniqueInput uniqueId,
        TeachersWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .SubjectsItems.Include(x => x.TeachersItems)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .TeachersItems.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();

        foreach (var child in children)
        {
            parent.TeachersItems?.Remove(child);
        }
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find multiple TeachersItems records for Subjects
    /// </summary>
    public async Task<List<Teachers>> FindTeachersItems(
        SubjectsWhereUniqueInput uniqueId,
        TeachersFindManyArgs subjectsFindManyArgs
    )
    {
        var teachersItems = await _context
            .TeachersItems.Where(m => m.SubjectId == uniqueId.Id)
            .ApplyWhere(subjectsFindManyArgs.Where)
            .ApplySkip(subjectsFindManyArgs.Skip)
            .ApplyTake(subjectsFindManyArgs.Take)
            .ApplyOrderBy(subjectsFindManyArgs.SortBy)
            .ToListAsync();

        return teachersItems.Select(x => x.ToDto()).ToList();
    }

    /// <summary>
    /// Update multiple TeachersItems records for Subjects
    /// </summary>
    public async Task UpdateTeachersItems(
        SubjectsWhereUniqueInput uniqueId,
        TeachersWhereUniqueInput[] childrenIds
    )
    {
        var subjects = await _context
            .SubjectsItems.Include(t => t.TeachersItems)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (subjects == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .TeachersItems.Where(a => childrenIds.Select(x => x.Id).Contains(a.Id))
            .ToListAsync();

        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        subjects.TeachersItems = children;
        await _context.SaveChangesAsync();
    }
}
