using Microsoft.AspNetCore.Mvc;
using Test.APIs;
using Test.APIs.Common;
using Test.APIs.Dtos;
using Test.APIs.Errors;

namespace Test.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class StudentsItemsControllerBase : ControllerBase
{
    protected readonly IStudentsItemsService _service;

    public StudentsItemsControllerBase(IStudentsItemsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Students
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<Students>> CreateStudents(StudentsCreateInput input)
    {
        var students = await _service.CreateStudents(input);

        return CreatedAtAction(nameof(Students), new { id = students.Id }, students);
    }

    /// <summary>
    /// Delete one Students
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteStudents([FromRoute()] StudentsWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteStudents(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many StudentsItems
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<Students>>> StudentsItems(
        [FromQuery()] StudentsFindManyArgs filter
    )
    {
        return Ok(await _service.StudentsItems(filter));
    }

    /// <summary>
    /// Meta data about Students records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> StudentsItemsMeta(
        [FromQuery()] StudentsFindManyArgs filter
    )
    {
        return Ok(await _service.StudentsItemsMeta(filter));
    }

    /// <summary>
    /// Get one Students
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<Students>> Students(
        [FromRoute()] StudentsWhereUniqueInput uniqueId
    )
    {
        try
        {
            return await _service.Students(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Students
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateStudents(
        [FromRoute()] StudentsWhereUniqueInput uniqueId,
        [FromQuery()] StudentsUpdateInput studentsUpdateDto
    )
    {
        try
        {
            await _service.UpdateStudents(uniqueId, studentsUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Connect multiple EnrollmentsItems records to Students
    /// </summary>
    [HttpPost("{Id}/enrollmentsItems")]
    public async Task<ActionResult> ConnectEnrollmentsItems(
        [FromRoute()] StudentsWhereUniqueInput uniqueId,
        [FromQuery()] EnrollmentsWhereUniqueInput[] enrollmentsItemsId
    )
    {
        try
        {
            await _service.ConnectEnrollmentsItems(uniqueId, enrollmentsItemsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Disconnect multiple EnrollmentsItems records from Students
    /// </summary>
    [HttpDelete("{Id}/enrollmentsItems")]
    public async Task<ActionResult> DisconnectEnrollmentsItems(
        [FromRoute()] StudentsWhereUniqueInput uniqueId,
        [FromBody()] EnrollmentsWhereUniqueInput[] enrollmentsItemsId
    )
    {
        try
        {
            await _service.DisconnectEnrollmentsItems(uniqueId, enrollmentsItemsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find multiple EnrollmentsItems records for Students
    /// </summary>
    [HttpGet("{Id}/enrollmentsItems")]
    public async Task<ActionResult<List<Enrollments>>> FindEnrollmentsItems(
        [FromRoute()] StudentsWhereUniqueInput uniqueId,
        [FromQuery()] EnrollmentsFindManyArgs filter
    )
    {
        try
        {
            return Ok(await _service.FindEnrollmentsItems(uniqueId, filter));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update multiple EnrollmentsItems records for Students
    /// </summary>
    [HttpPatch("{Id}/enrollmentsItems")]
    public async Task<ActionResult> UpdateEnrollmentsItems(
        [FromRoute()] StudentsWhereUniqueInput uniqueId,
        [FromBody()] EnrollmentsWhereUniqueInput[] enrollmentsItemsId
    )
    {
        try
        {
            await _service.UpdateEnrollmentsItems(uniqueId, enrollmentsItemsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
