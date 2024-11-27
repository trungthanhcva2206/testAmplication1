using Microsoft.AspNetCore.Mvc;

namespace Test.APIs;

[ApiController()]
public class TeachersItemsController : TeachersItemsControllerBase
{
    public TeachersItemsController(ITeachersItemsService service)
        : base(service) { }
}
