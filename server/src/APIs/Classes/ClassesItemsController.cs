using Microsoft.AspNetCore.Mvc;

namespace Test.APIs;

[ApiController()]
public class ClassesItemsController : ClassesItemsControllerBase
{
    public ClassesItemsController(IClassesItemsService service)
        : base(service) { }
}
