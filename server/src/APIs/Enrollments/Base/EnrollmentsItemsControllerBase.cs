using Microsoft.AspNetCore.Mvc;
using Test.APIs;
using Test.APIs.Common;
using Test.APIs.Dtos;
using Test.APIs.Errors;

namespace Test.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class EnrollmentsItemsControllerBase : ControllerBase
{
    protected readonly IEnrollmentsItemsService _service;

    public EnrollmentsItemsControllerBase(IEnrollmentsItemsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Enrollments
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<Enrollments>> CreateEnrollments(EnrollmentsCreateInput input)
    {
        var enrollments = await _service.CreateEnrollments(input);

        return CreatedAtAction(nameof(Enrollments), new { id = enrollments.Id }, enrollments);
    }

    /// <summary>
    /// Delete one Enrollments
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteEnrollments(
        [FromRoute()] EnrollmentsWhereUniqueInput uniqueId
    )
    {
        try
        {
            await _service.DeleteEnrollments(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many EnrollmentsItems
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<Enrollments>>> EnrollmentsItems(
        [FromQuery()] EnrollmentsFindManyArgs filter
    )
    {
        return Ok(await _service.EnrollmentsItems(filter));
    }

    /// <summary>
    /// Meta data about Enrollments records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> EnrollmentsItemsMeta(
        [FromQuery()] EnrollmentsFindManyArgs filter
    )
    {
        return Ok(await _service.EnrollmentsItemsMeta(filter));
    }

    /// <summary>
    /// Get one Enrollments
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<Enrollments>> Enrollments(
        [FromRoute()] EnrollmentsWhereUniqueInput uniqueId
    )
    {
        try
        {
            return await _service.Enrollments(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Enrollments
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateEnrollments(
        [FromRoute()] EnrollmentsWhereUniqueInput uniqueId,
        [FromQuery()] EnrollmentsUpdateInput enrollmentsUpdateDto
    )
    {
        try
        {
            await _service.UpdateEnrollments(uniqueId, enrollmentsUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Get a class record for Enrollments
    /// </summary>
    [HttpGet("{Id}/classField")]
    public async Task<ActionResult<List<Classes>>> GetClassField(
        [FromRoute()] EnrollmentsWhereUniqueInput uniqueId
    )
    {
        var classes = await _service.GetClassField(uniqueId);
        return Ok(classes);
    }

    /// <summary>
    /// Get a student record for Enrollments
    /// </summary>
    [HttpGet("{Id}/student")]
    public async Task<ActionResult<List<Students>>> GetStudent(
        [FromRoute()] EnrollmentsWhereUniqueInput uniqueId
    )
    {
        var students = await _service.GetStudent(uniqueId);
        return Ok(students);
    }
}
