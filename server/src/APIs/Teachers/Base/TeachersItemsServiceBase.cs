using Microsoft.EntityFrameworkCore;
using Test.APIs;
using Test.APIs.Common;
using Test.APIs.Dtos;
using Test.APIs.Errors;
using Test.APIs.Extensions;
using Test.Infrastructure;
using Test.Infrastructure.Models;

namespace Test.APIs;

public abstract class TeachersItemsServiceBase : ITeachersItemsService
{
    protected readonly TestDbContext _context;

    public TeachersItemsServiceBase(TestDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Teachers
    /// </summary>
    public async Task<Teachers> CreateTeachers(TeachersCreateInput createDto)
    {
        var teachers = new TeachersDbModel
        {
            CreatedAt = createDto.CreatedAt,
            FirstName = createDto.FirstName,
            LastName = createDto.LastName,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            teachers.Id = createDto.Id;
        }
        if (createDto.ClassesItems != null)
        {
            teachers.ClassesItems = await _context
                .ClassesItems.Where(classes =>
                    createDto.ClassesItems.Select(t => t.Id).Contains(classes.Id)
                )
                .ToListAsync();
        }

        if (createDto.Subject != null)
        {
            teachers.Subject = await _context
                .SubjectsItems.Where(subjects => createDto.Subject.Id == subjects.Id)
                .FirstOrDefaultAsync();
        }

        _context.TeachersItems.Add(teachers);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<TeachersDbModel>(teachers.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Teachers
    /// </summary>
    public async Task DeleteTeachers(TeachersWhereUniqueInput uniqueId)
    {
        var teachers = await _context.TeachersItems.FindAsync(uniqueId.Id);
        if (teachers == null)
        {
            throw new NotFoundException();
        }

        _context.TeachersItems.Remove(teachers);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many TeachersItems
    /// </summary>
    public async Task<List<Teachers>> TeachersItems(TeachersFindManyArgs findManyArgs)
    {
        var teachersItems = await _context
            .TeachersItems.Include(x => x.ClassesItems)
            .Include(x => x.Subject)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return teachersItems.ConvertAll(teachers => teachers.ToDto());
    }

    /// <summary>
    /// Meta data about Teachers records
    /// </summary>
    public async Task<MetadataDto> TeachersItemsMeta(TeachersFindManyArgs findManyArgs)
    {
        var count = await _context.TeachersItems.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Teachers
    /// </summary>
    public async Task<Teachers> Teachers(TeachersWhereUniqueInput uniqueId)
    {
        var teachersItems = await this.TeachersItems(
            new TeachersFindManyArgs { Where = new TeachersWhereInput { Id = uniqueId.Id } }
        );
        var teachers = teachersItems.FirstOrDefault();
        if (teachers == null)
        {
            throw new NotFoundException();
        }

        return teachers;
    }

    /// <summary>
    /// Update one Teachers
    /// </summary>
    public async Task UpdateTeachers(
        TeachersWhereUniqueInput uniqueId,
        TeachersUpdateInput updateDto
    )
    {
        var teachers = updateDto.ToModel(uniqueId);

        if (updateDto.ClassesItems != null)
        {
            teachers.ClassesItems = await _context
                .ClassesItems.Where(classes =>
                    updateDto.ClassesItems.Select(t => t).Contains(classes.Id)
                )
                .ToListAsync();
        }

        if (updateDto.Subject != null)
        {
            teachers.Subject = await _context
                .SubjectsItems.Where(subjects => updateDto.Subject == subjects.Id)
                .FirstOrDefaultAsync();
        }

        _context.Entry(teachers).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.TeachersItems.Any(e => e.Id == teachers.Id))
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
    /// Connect multiple ClassesItems records to Teachers
    /// </summary>
    public async Task ConnectClassesItems(
        TeachersWhereUniqueInput uniqueId,
        ClassesWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .TeachersItems.Include(x => x.ClassesItems)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .ClassesItems.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();
        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        var childrenToConnect = children.Except(parent.ClassesItems);

        foreach (var child in childrenToConnect)
        {
            parent.ClassesItems.Add(child);
        }

        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Disconnect multiple ClassesItems records from Teachers
    /// </summary>
    public async Task DisconnectClassesItems(
        TeachersWhereUniqueInput uniqueId,
        ClassesWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .TeachersItems.Include(x => x.ClassesItems)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .ClassesItems.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();

        foreach (var child in children)
        {
            parent.ClassesItems?.Remove(child);
        }
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find multiple ClassesItems records for Teachers
    /// </summary>
    public async Task<List<Classes>> FindClassesItems(
        TeachersWhereUniqueInput uniqueId,
        ClassesFindManyArgs teachersFindManyArgs
    )
    {
        var classesItems = await _context
            .ClassesItems.Where(m => m.TeacherId == uniqueId.Id)
            .ApplyWhere(teachersFindManyArgs.Where)
            .ApplySkip(teachersFindManyArgs.Skip)
            .ApplyTake(teachersFindManyArgs.Take)
            .ApplyOrderBy(teachersFindManyArgs.SortBy)
            .ToListAsync();

        return classesItems.Select(x => x.ToDto()).ToList();
    }

    /// <summary>
    /// Update multiple ClassesItems records for Teachers
    /// </summary>
    public async Task UpdateClassesItems(
        TeachersWhereUniqueInput uniqueId,
        ClassesWhereUniqueInput[] childrenIds
    )
    {
        var teachers = await _context
            .TeachersItems.Include(t => t.ClassesItems)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (teachers == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .ClassesItems.Where(a => childrenIds.Select(x => x.Id).Contains(a.Id))
            .ToListAsync();

        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        teachers.ClassesItems = children;
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Get a subject record for Teachers
    /// </summary>
    public async Task<Subjects> GetSubject(TeachersWhereUniqueInput uniqueId)
    {
        var teachers = await _context
            .TeachersItems.Where(teachers => teachers.Id == uniqueId.Id)
            .Include(teachers => teachers.Subject)
            .FirstOrDefaultAsync();
        if (teachers == null)
        {
            throw new NotFoundException();
        }
        return teachers.Subject.ToDto();
    }
}
