using Test.Infrastructure;

namespace Test.APIs;

public class StudentsItemsService : StudentsItemsServiceBase
{
    public StudentsItemsService(TestDbContext context)
        : base(context) { }
}
