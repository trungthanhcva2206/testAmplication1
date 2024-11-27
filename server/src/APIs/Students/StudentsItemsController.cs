using Microsoft.AspNetCore.Mvc;

namespace Test.APIs;

[ApiController()]
public class StudentsItemsController : StudentsItemsControllerBase
{
    public StudentsItemsController(IStudentsItemsService service)
        : base(service) { }
}
