using Microsoft.AspNetCore.Mvc;
using Test.APIs;
using Test.APIs.Common;
using Test.APIs.Dtos;
using Test.APIs.Errors;

namespace Test.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class ClassesItemsControllerBase : ControllerBase
{
    protected readonly IClassesItemsService _service;

    public ClassesItemsControllerBase(IClassesItemsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Classes
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<Classes>> CreateClasses(ClassesCreateInput input)
    {
        var classes = await _service.CreateClasses(input);

        return CreatedAtAction(nameof(Classes), new { id = classes.Id }, classes);
    }

    /// <summary>
    /// Delete one Classes
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteClasses([FromRoute()] ClassesWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteClasses(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many ClassesItems
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<Classes>>> ClassesItems(
        [FromQuery()] ClassesFindManyArgs filter
    )
    {
        return Ok(await _service.ClassesItems(filter));
    }

    /// <summary>
    /// Meta data about Classes records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> ClassesItemsMeta(
        [FromQuery()] ClassesFindManyArgs filter
    )
    {
        return Ok(await _service.ClassesItemsMeta(filter));
    }

    /// <summary>
    /// Get one Classes
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<Classes>> Classes([FromRoute()] ClassesWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.Classes(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Classes
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateClasses(
        [FromRoute()] ClassesWhereUniqueInput uniqueId,
        [FromQuery()] ClassesUpdateInput classesUpdateDto
    )
    {
        try
        {
            await _service.UpdateClasses(uniqueId, classesUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Connect multiple EnrollmentsItems records to Classes
    /// </summary>
    [HttpPost("{Id}/enrollmentsItems")]
    public async Task<ActionResult> ConnectEnrollmentsItems(
        [FromRoute()] ClassesWhereUniqueInput uniqueId,
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
    /// Disconnect multiple EnrollmentsItems records from Classes
    /// </summary>
    [HttpDelete("{Id}/enrollmentsItems")]
    public async Task<ActionResult> DisconnectEnrollmentsItems(
        [FromRoute()] ClassesWhereUniqueInput uniqueId,
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
    /// Find multiple EnrollmentsItems records for Classes
    /// </summary>
    [HttpGet("{Id}/enrollmentsItems")]
    public async Task<ActionResult<List<Enrollments>>> FindEnrollmentsItems(
        [FromRoute()] ClassesWhereUniqueInput uniqueId,
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
    /// Update multiple EnrollmentsItems records for Classes
    /// </summary>
    [HttpPatch("{Id}/enrollmentsItems")]
    public async Task<ActionResult> UpdateEnrollmentsItems(
        [FromRoute()] ClassesWhereUniqueInput uniqueId,
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

    /// <summary>
    /// Get a teacher record for Classes
    /// </summary>
    [HttpGet("{Id}/teacher")]
    public async Task<ActionResult<List<Teachers>>> GetTeacher(
        [FromRoute()] ClassesWhereUniqueInput uniqueId
    )
    {
        var teachers = await _service.GetTeacher(uniqueId);
        return Ok(teachers);
    }
}
