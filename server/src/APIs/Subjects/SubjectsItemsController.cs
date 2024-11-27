using Microsoft.AspNetCore.Mvc;

namespace Test.APIs;

[ApiController()]
public class SubjectsItemsController : SubjectsItemsControllerBase
{
    public SubjectsItemsController(ISubjectsItemsService service)
        : base(service) { }
}
