using Test.APIs;

namespace Test;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Add services to the container.
    /// </summary>
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IClassesService, ClassesService>();
        services.AddScoped<IEnrollmentsService, EnrollmentsService>();
        services.AddScoped<IStudentsService, StudentsService>();
        services.AddScoped<ISubjectsService, SubjectsService>();
        services.AddScoped<ITeachersService, TeachersService>();
    }
}
