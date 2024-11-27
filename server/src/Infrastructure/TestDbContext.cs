using Microsoft.EntityFrameworkCore;
using Test.Infrastructure.Models;

namespace Test.Infrastructure;

public class TestDbContext : DbContext
{
    public TestDbContext(DbContextOptions<TestDbContext> options)
        : base(options) { }

    public DbSet<StudentsDbModel> StudentsItems { get; set; }

    public DbSet<TeachersDbModel> TeachersItems { get; set; }

    public DbSet<ClassesDbModel> ClassesItems { get; set; }

    public DbSet<SubjectsDbModel> SubjectsItems { get; set; }

    public DbSet<EnrollmentsDbModel> EnrollmentsItems { get; set; }
}
