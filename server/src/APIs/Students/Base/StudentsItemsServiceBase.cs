using Microsoft.EntityFrameworkCore;
using Test.APIs;
using Test.APIs.Common;
using Test.APIs.Dtos;
using Test.APIs.Errors;
using Test.APIs.Extensions;
using Test.Infrastructure;
using Test.Infrastructure.Models;

namespace Test.APIs;

public abstract class StudentsItemsServiceBase : IStudentsItemsService
{
    protected readonly TestDbContext _context;

    public StudentsItemsServiceBase(TestDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Students
    /// </summary>
    public async Task<Students> CreateStudents(StudentsCreateInput createDto)
    {
        var students = new StudentsDbModel
        {
            CreatedAt = createDto.CreatedAt,
            Email = createDto.Email,
            EnrollmentDate = createDto.EnrollmentDate,
            FirstName = createDto.FirstName,
            GradeLevel = createDto.GradeLevel,
            LastName = createDto.LastName,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            students.Id = createDto.Id;
        }
        if (createDto.EnrollmentsItems != null)
        {
            students.EnrollmentsItems = await _context
                .EnrollmentsItems.Where(enrollments =>
                    createDto.EnrollmentsItems.Select(t => t.Id).Contains(enrollments.Id)
                )
                .ToListAsync();
        }

        _context.StudentsItems.Add(students);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<StudentsDbModel>(students.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Students
    /// </summary>
    public async Task DeleteStudents(StudentsWhereUniqueInput uniqueId)
    {
        var students = await _context.StudentsItems.FindAsync(uniqueId.Id);
        if (students == null)
        {
            throw new NotFoundException();
        }

        _context.StudentsItems.Remove(students);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many StudentsItems
    /// </summary>
    public async Task<List<Students>> StudentsItems(StudentsFindManyArgs findManyArgs)
    {
        var studentsItems = await _context
            .StudentsItems.Include(x => x.EnrollmentsItems)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return studentsItems.ConvertAll(students => students.ToDto());
    }

    /// <summary>
    /// Meta data about Students records
    /// </summary>
    public async Task<MetadataDto> StudentsItemsMeta(StudentsFindManyArgs findManyArgs)
    {
        var count = await _context.StudentsItems.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Students
    /// </summary>
    public async Task<Students> Students(StudentsWhereUniqueInput uniqueId)
    {
        var studentsItems = await this.StudentsItems(
            new StudentsFindManyArgs { Where = new StudentsWhereInput { Id = uniqueId.Id } }
        );
        var students = studentsItems.FirstOrDefault();
        if (students == null)
        {
            throw new NotFoundException();
        }

        return students;
    }

    /// <summary>
    /// Update one Students
    /// </summary>
    public async Task UpdateStudents(
        StudentsWhereUniqueInput uniqueId,
        StudentsUpdateInput updateDto
    )
    {
        var students = updateDto.ToModel(uniqueId);

        if (updateDto.EnrollmentsItems != null)
        {
            students.EnrollmentsItems = await _context
                .EnrollmentsItems.Where(enrollments =>
                    updateDto.EnrollmentsItems.Select(t => t).Contains(enrollments.Id)
                )
                .ToListAsync();
        }

        _context.Entry(students).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.StudentsItems.Any(e => e.Id == students.Id))
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
    /// Connect multiple EnrollmentsItems records to Students
    /// </summary>
    public async Task ConnectEnrollmentsItems(
        StudentsWhereUniqueInput uniqueId,
        EnrollmentsWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .StudentsItems.Include(x => x.EnrollmentsItems)
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
    /// Disconnect multiple EnrollmentsItems records from Students
    /// </summary>
    public async Task DisconnectEnrollmentsItems(
        StudentsWhereUniqueInput uniqueId,
        EnrollmentsWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .StudentsItems.Include(x => x.EnrollmentsItems)
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
    /// Find multiple EnrollmentsItems records for Students
    /// </summary>
    public async Task<List<Enrollments>> FindEnrollmentsItems(
        StudentsWhereUniqueInput uniqueId,
        EnrollmentsFindManyArgs studentsFindManyArgs
    )
    {
        var enrollmentsItems = await _context
            .EnrollmentsItems.Where(m => m.StudentId == uniqueId.Id)
            .ApplyWhere(studentsFindManyArgs.Where)
            .ApplySkip(studentsFindManyArgs.Skip)
            .ApplyTake(studentsFindManyArgs.Take)
            .ApplyOrderBy(studentsFindManyArgs.SortBy)
            .ToListAsync();

        return enrollmentsItems.Select(x => x.ToDto()).ToList();
    }

    /// <summary>
    /// Update multiple EnrollmentsItems records for Students
    /// </summary>
    public async Task UpdateEnrollmentsItems(
        StudentsWhereUniqueInput uniqueId,
        EnrollmentsWhereUniqueInput[] childrenIds
    )
    {
        var students = await _context
            .StudentsItems.Include(t => t.EnrollmentsItems)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (students == null)
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

        students.EnrollmentsItems = children;
        await _context.SaveChangesAsync();
    }
}
