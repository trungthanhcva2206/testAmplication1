using Microsoft.AspNetCore.Mvc;
using Test.APIs;
using Test.APIs.Common;
using Test.APIs.Dtos;
using Test.APIs.Errors;

namespace Test.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class TeachersItemsControllerBase : ControllerBase
{
    protected readonly ITeachersItemsService _service;

    public TeachersItemsControllerBase(ITeachersItemsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Teachers
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<Teachers>> CreateTeachers(TeachersCreateInput input)
    {
        var teachers = await _service.CreateTeachers(input);

        return CreatedAtAction(nameof(Teachers), new { id = teachers.Id }, teachers);
    }

    /// <summary>
    /// Delete one Teachers
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteTeachers([FromRoute()] TeachersWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteTeachers(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many TeachersItems
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<Teachers>>> TeachersItems(
        [FromQuery()] TeachersFindManyArgs filter
    )
    {
        return Ok(await _service.TeachersItems(filter));
    }

    /// <summary>
    /// Meta data about Teachers records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> TeachersItemsMeta(
        [FromQuery()] TeachersFindManyArgs filter
    )
    {
        return Ok(await _service.TeachersItemsMeta(filter));
    }

    /// <summary>
    /// Get one Teachers
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<Teachers>> Teachers(
        [FromRoute()] TeachersWhereUniqueInput uniqueId
    )
    {
        try
        {
            return await _service.Teachers(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Teachers
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateTeachers(
        [FromRoute()] TeachersWhereUniqueInput uniqueId,
        [FromQuery()] TeachersUpdateInput teachersUpdateDto
    )
    {
        try
        {
            await _service.UpdateTeachers(uniqueId, teachersUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Connect multiple ClassesItems records to Teachers
    /// </summary>
    [HttpPost("{Id}/classesItems")]
    public async Task<ActionResult> ConnectClassesItems(
        [FromRoute()] TeachersWhereUniqueInput uniqueId,
        [FromQuery()] ClassesWhereUniqueInput[] classesItemsId
    )
    {
        try
        {
            await _service.ConnectClassesItems(uniqueId, classesItemsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Disconnect multiple ClassesItems records from Teachers
    /// </summary>
    [HttpDelete("{Id}/classesItems")]
    public async Task<ActionResult> DisconnectClassesItems(
        [FromRoute()] TeachersWhereUniqueInput uniqueId,
        [FromBody()] ClassesWhereUniqueInput[] classesItemsId
    )
    {
        try
        {
            await _service.DisconnectClassesItems(uniqueId, classesItemsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find multiple ClassesItems records for Teachers
    /// </summary>
    [HttpGet("{Id}/classesItems")]
    public async Task<ActionResult<List<Classes>>> FindClassesItems(
        [FromRoute()] TeachersWhereUniqueInput uniqueId,
        [FromQuery()] ClassesFindManyArgs filter
    )
    {
        try
        {
            return Ok(await _service.FindClassesItems(uniqueId, filter));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update multiple ClassesItems records for Teachers
    /// </summary>
    [HttpPatch("{Id}/classesItems")]
    public async Task<ActionResult> UpdateClassesItems(
        [FromRoute()] TeachersWhereUniqueInput uniqueId,
        [FromBody()] ClassesWhereUniqueInput[] classesItemsId
    )
    {
        try
        {
            await _service.UpdateClassesItems(uniqueId, classesItemsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Get a subject record for Teachers
    /// </summary>
    [HttpGet("{Id}/subject")]
    public async Task<ActionResult<List<Subjects>>> GetSubject(
        [FromRoute()] TeachersWhereUniqueInput uniqueId
    )
    {
        var subjects = await _service.GetSubject(uniqueId);
        return Ok(subjects);
    }
}
