using Microsoft.AspNetCore.Mvc;
using Test.APIs;
using Test.APIs.Common;
using Test.APIs.Dtos;
using Test.APIs.Errors;

namespace Test.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class SubjectsItemsControllerBase : ControllerBase
{
    protected readonly ISubjectsItemsService _service;

    public SubjectsItemsControllerBase(ISubjectsItemsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Subjects
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<Subjects>> CreateSubjects(SubjectsCreateInput input)
    {
        var subjects = await _service.CreateSubjects(input);

        return CreatedAtAction(nameof(Subjects), new { id = subjects.Id }, subjects);
    }

    /// <summary>
    /// Delete one Subjects
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteSubjects([FromRoute()] SubjectsWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteSubjects(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many SubjectsItems
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<Subjects>>> SubjectsItems(
        [FromQuery()] SubjectsFindManyArgs filter
    )
    {
        return Ok(await _service.SubjectsItems(filter));
    }

    /// <summary>
    /// Meta data about Subjects records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> SubjectsItemsMeta(
        [FromQuery()] SubjectsFindManyArgs filter
    )
    {
        return Ok(await _service.SubjectsItemsMeta(filter));
    }

    /// <summary>
    /// Get one Subjects
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<Subjects>> Subjects(
        [FromRoute()] SubjectsWhereUniqueInput uniqueId
    )
    {
        try
        {
            return await _service.Subjects(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Subjects
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateSubjects(
        [FromRoute()] SubjectsWhereUniqueInput uniqueId,
        [FromQuery()] SubjectsUpdateInput subjectsUpdateDto
    )
    {
        try
        {
            await _service.UpdateSubjects(uniqueId, subjectsUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Connect multiple TeachersItems records to Subjects
    /// </summary>
    [HttpPost("{Id}/teachersItems")]
    public async Task<ActionResult> ConnectTeachersItems(
        [FromRoute()] SubjectsWhereUniqueInput uniqueId,
        [FromQuery()] TeachersWhereUniqueInput[] teachersItemsId
    )
    {
        try
        {
            await _service.ConnectTeachersItems(uniqueId, teachersItemsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Disconnect multiple TeachersItems records from Subjects
    /// </summary>
    [HttpDelete("{Id}/teachersItems")]
    public async Task<ActionResult> DisconnectTeachersItems(
        [FromRoute()] SubjectsWhereUniqueInput uniqueId,
        [FromBody()] TeachersWhereUniqueInput[] teachersItemsId
    )
    {
        try
        {
            await _service.DisconnectTeachersItems(uniqueId, teachersItemsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find multiple TeachersItems records for Subjects
    /// </summary>
    [HttpGet("{Id}/teachersItems")]
    public async Task<ActionResult<List<Teachers>>> FindTeachersItems(
        [FromRoute()] SubjectsWhereUniqueInput uniqueId,
        [FromQuery()] TeachersFindManyArgs filter
    )
    {
        try
        {
            return Ok(await _service.FindTeachersItems(uniqueId, filter));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update multiple TeachersItems records for Subjects
    /// </summary>
    [HttpPatch("{Id}/teachersItems")]
    public async Task<ActionResult> UpdateTeachersItems(
        [FromRoute()] SubjectsWhereUniqueInput uniqueId,
        [FromBody()] TeachersWhereUniqueInput[] teachersItemsId
    )
    {
        try
        {
            await _service.UpdateTeachersItems(uniqueId, teachersItemsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
