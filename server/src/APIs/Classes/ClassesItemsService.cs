using Test.Infrastructure;

namespace Test.APIs;

public class ClassesItemsService : ClassesItemsServiceBase
{
    public ClassesItemsService(TestDbContext context)
        : base(context) { }
}
