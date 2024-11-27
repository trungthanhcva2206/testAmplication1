using Test.Infrastructure;

namespace Test.APIs;

public class SubjectsItemsService : SubjectsItemsServiceBase
{
    public SubjectsItemsService(TestDbContext context)
        : base(context) { }
}
