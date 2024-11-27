using Microsoft.EntityFrameworkCore;
using Test.APIs;
using Test.APIs.Common;
using Test.APIs.Dtos;
using Test.APIs.Errors;
using Test.APIs.Extensions;
using Test.Infrastructure;
using Test.Infrastructure.Models;

namespace Test.APIs;

public abstract class EnrollmentsItemsServiceBase : IEnrollmentsItemsService
{
    protected readonly TestDbContext _context;

    public EnrollmentsItemsServiceBase(TestDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Enrollments
    /// </summary>
    public async Task<Enrollments> CreateEnrollments(EnrollmentsCreateInput createDto)
    {
        var enrollments = new EnrollmentsDbModel
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            enrollments.Id = createDto.Id;
        }
        if (createDto.ClassField != null)
        {
            enrollments.ClassField = await _context
                .ClassesItems.Where(classes => createDto.ClassField.Id == classes.Id)
                .FirstOrDefaultAsync();
        }

        if (createDto.Student != null)
        {
            enrollments.Student = await _context
                .StudentsItems.Where(students => createDto.Student.Id == students.Id)
                .FirstOrDefaultAsync();
        }

        _context.EnrollmentsItems.Add(enrollments);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<EnrollmentsDbModel>(enrollments.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Enrollments
    /// </summary>
    public async Task DeleteEnrollments(EnrollmentsWhereUniqueInput uniqueId)
    {
        var enrollments = await _context.EnrollmentsItems.FindAsync(uniqueId.Id);
        if (enrollments == null)
        {
            throw new NotFoundException();
        }

        _context.EnrollmentsItems.Remove(enrollments);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many EnrollmentsItems
    /// </summary>
    public async Task<List<Enrollments>> EnrollmentsItems(EnrollmentsFindManyArgs findManyArgs)
    {
        var enrollmentsItems = await _context
            .EnrollmentsItems.Include(x => x.Student)
            .Include(x => x.ClassField)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return enrollmentsItems.ConvertAll(enrollments => enrollments.ToDto());
    }

    /// <summary>
    /// Meta data about Enrollments records
    /// </summary>
    public async Task<MetadataDto> EnrollmentsItemsMeta(EnrollmentsFindManyArgs findManyArgs)
    {
        var count = await _context.EnrollmentsItems.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Enrollments
    /// </summary>
    public async Task<Enrollments> Enrollments(EnrollmentsWhereUniqueInput uniqueId)
    {
        var enrollmentsItems = await this.EnrollmentsItems(
            new EnrollmentsFindManyArgs { Where = new EnrollmentsWhereInput { Id = uniqueId.Id } }
        );
        var enrollments = enrollmentsItems.FirstOrDefault();
        if (enrollments == null)
        {
            throw new NotFoundException();
        }

        return enrollments;
    }

    /// <summary>
    /// Update one Enrollments
    /// </summary>
    public async Task UpdateEnrollments(
        EnrollmentsWhereUniqueInput uniqueId,
        EnrollmentsUpdateInput updateDto
    )
    {
        var enrollments = updateDto.ToModel(uniqueId);

        if (updateDto.ClassField != null)
        {
            enrollments.ClassField = await _context
                .ClassesItems.Where(classes => updateDto.ClassField == classes.Id)
                .FirstOrDefaultAsync();
        }

        if (updateDto.Student != null)
        {
            enrollments.Student = await _context
                .StudentsItems.Where(students => updateDto.Student == students.Id)
                .FirstOrDefaultAsync();
        }

        _context.Entry(enrollments).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.EnrollmentsItems.Any(e => e.Id == enrollments.Id))
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
    /// Get a class record for Enrollments
    /// </summary>
    public async Task<Classes> GetClassField(EnrollmentsWhereUniqueInput uniqueId)
    {
        var enrollments = await _context
            .EnrollmentsItems.Where(enrollments => enrollments.Id == uniqueId.Id)
            .Include(enrollments => enrollments.ClassField)
            .FirstOrDefaultAsync();
        if (enrollments == null)
        {
            throw new NotFoundException();
        }
        return enrollments.ClassField.ToDto();
    }

    /// <summary>
    /// Get a student record for Enrollments
    /// </summary>
    public async Task<Students> GetStudent(EnrollmentsWhereUniqueInput uniqueId)
    {
        var enrollments = await _context
            .EnrollmentsItems.Where(enrollments => enrollments.Id == uniqueId.Id)
            .Include(enrollments => enrollments.Student)
            .FirstOrDefaultAsync();
        if (enrollments == null)
        {
            throw new NotFoundException();
        }
        return enrollments.Student.ToDto();
    }
}
