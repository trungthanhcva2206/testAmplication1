using Microsoft.EntityFrameworkCore;
using Test.APIs;
using Test.APIs.Common;
using Test.APIs.Dtos;
using Test.APIs.Errors;
using Test.APIs.Extensions;
using Test.Infrastructure;
using Test.Infrastructure.Models;

namespace Test.APIs;

public abstract class ClassesItemsServiceBase : IClassesItemsService
{
    protected readonly TestDbContext _context;

    public ClassesItemsServiceBase(TestDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Classes
    /// </summary>
    public async Task<Classes> CreateClasses(ClassesCreateInput createDto)
    {
        var classes = new ClassesDbModel
        {
            ClassName = createDto.ClassName,
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            classes.Id = createDto.Id;
        }
        if (createDto.EnrollmentsItems != null)
        {
            classes.EnrollmentsItems = await _context
                .EnrollmentsItems.Where(enrollments =>
                    createDto.EnrollmentsItems.Select(t => t.Id).Contains(enrollments.Id)
                )
                .ToListAsync();
        }

        if (createDto.Teacher != null)
        {
            classes.Teacher = await _context
                .TeachersItems.Where(teachers => createDto.Teacher.Id == teachers.Id)
                .FirstOrDefaultAsync();
        }

        _context.ClassesItems.Add(classes);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<ClassesDbModel>(classes.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Classes
    /// </summary>
    public async Task DeleteClasses(ClassesWhereUniqueInput uniqueId)
    {
        var classes = await _context.ClassesItems.FindAsync(uniqueId.Id);
        if (classes == null)
        {
            throw new NotFoundException();
        }

        _context.ClassesItems.Remove(classes);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many ClassesItems
    /// </summary>
    public async Task<List<Classes>> ClassesItems(ClassesFindManyArgs findManyArgs)
    {
        var classesItems = await _context
            .ClassesItems.Include(x => x.Teacher)
            .Include(x => x.EnrollmentsItems)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return classesItems.ConvertAll(classes => classes.ToDto());
    }

    /// <summary>
    /// Meta data about Classes records
    /// </summary>
    public async Task<MetadataDto> ClassesItemsMeta(ClassesFindManyArgs findManyArgs)
    {
        var count = await _context.ClassesItems.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Classes
    /// </summary>
    public async Task<Classes> Classes(ClassesWhereUniqueInput uniqueId)
    {
        var classesItems = await this.ClassesItems(
            new ClassesFindManyArgs { Where = new ClassesWhereInput { Id = uniqueId.Id } }
        );
        var classes = classesItems.FirstOrDefault();
        if (classes == null)
        {
            throw new NotFoundException();
        }

        return classes;
    }

    /// <summary>
    /// Update one Classes
    /// </summary>
    public async Task UpdateClasses(ClassesWhereUniqueInput uniqueId, ClassesUpdateInput updateDto)
    {
        var classes = updateDto.ToModel(uniqueId);

        if (updateDto.EnrollmentsItems != null)
        {
            classes.EnrollmentsItems = await _context
                .EnrollmentsItems.Where(enrollments =>
                    updateDto.EnrollmentsItems.Select(t => t).Contains(enrollments.Id)
                )
                .ToListAsync();
        }

        if (updateDto.Teacher != null)
        {
            classes.Teacher = await _context
                .TeachersItems.Where(teachers => updateDto.Teacher == teachers.Id)
                .FirstOrDefaultAsync();
        }

        _context.Entry(classes).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.ClassesItems.Any(e => e.Id == classes.Id))
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
    /// Connect multiple EnrollmentsItems records to Classes
    /// </summary>
    public async Task ConnectEnrollmentsItems(
        ClassesWhereUniqueInput uniqueId,
        EnrollmentsWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .ClassesItems.Include(x => x.EnrollmentsItems)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .EnrollmentsItems.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();
        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        var childrenToConnect = children.Except(parent.EnrollmentsItems);

        foreach (var child in childrenToConnect)
        {
            parent.EnrollmentsItems.Add(child);
        }

        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Disconnect multiple EnrollmentsItems records from Classes
    /// </summary>
    public async Task DisconnectEnrollmentsItems(
        ClassesWhereUniqueInput uniqueId,
        EnrollmentsWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .ClassesItems.Include(x => x.EnrollmentsItems)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .EnrollmentsItems.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();

        foreach (var child in children)
        {
            parent.EnrollmentsItems?.Remove(child);
        }
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find multiple EnrollmentsItems records for Classes
    /// </summary>
    public async Task<List<Enrollments>> FindEnrollmentsItems(
        ClassesWhereUniqueInput uniqueId,
        EnrollmentsFindManyArgs classesFindManyArgs
    )
    {
        var enrollmentsItems = await _context
            .EnrollmentsItems.Where(m => m.ClassFieldId == uniqueId.Id)
            .ApplyWhere(classesFindManyArgs.Where)
            .ApplySkip(classesFindManyArgs.Skip)
            .ApplyTake(classesFindManyArgs.Take)
            .ApplyOrderBy(classesFindManyArgs.SortBy)
            .ToListAsync();

        return enrollmentsItems.Select(x => x.ToDto()).ToList();
    }

    /// <summary>
    /// Update multiple EnrollmentsItems records for Classes
    /// </summary>
    public async Task UpdateEnrollmentsItems(
        ClassesWhereUniqueInput uniqueId,
        EnrollmentsWhereUniqueInput[] childrenIds
    )
    {
        var classes = await _context
            .ClassesItems.Include(t => t.EnrollmentsItems)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (classes == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .EnrollmentsItems.Where(a => childrenIds.Select(x => x.Id).Contains(a.Id))
            .ToListAsync();

        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        classes.EnrollmentsItems = children;
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Get a teacher record for Classes
    /// </summary>
    public async Task<Teachers> GetTeacher(ClassesWhereUniqueInput uniqueId)
    {
        var classes = await _context
            .ClassesItems.Where(classes => classes.Id == uniqueId.Id)
            .Include(classes => classes.Teacher)
            .FirstOrDefaultAsync();
        if (classes == null)
        {
            throw new NotFoundException();
        }
        return classes.Teacher.ToDto();
    }
}
