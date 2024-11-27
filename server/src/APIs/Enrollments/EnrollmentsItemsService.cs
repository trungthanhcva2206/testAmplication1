using Test.Infrastructure;

namespace Test.APIs;

public class EnrollmentsItemsService : EnrollmentsItemsServiceBase
{
    public EnrollmentsItemsService(TestDbContext context)
        : base(context) { }
}
