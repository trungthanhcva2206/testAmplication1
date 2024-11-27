using Test.Infrastructure;

namespace Test.APIs;

public class TeachersItemsService : TeachersItemsServiceBase
{
    public TeachersItemsService(TestDbContext context)
        : base(context) { }
}
