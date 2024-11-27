using Microsoft.AspNetCore.Mvc;

namespace Test.APIs;

[ApiController()]
public class EnrollmentsItemsController : EnrollmentsItemsControllerBase
{
    public EnrollmentsItemsController(IEnrollmentsItemsService service)
        : base(service) { }
}
