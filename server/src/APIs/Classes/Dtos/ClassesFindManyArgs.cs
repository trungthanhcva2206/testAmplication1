using Microsoft.AspNetCore.Mvc;
using Test.APIs.Common;
using Test.Infrastructure.Models;

namespace Test.APIs.Dtos;

[BindProperties(SupportsGet = true)]
public class ClassesFindManyArgs : FindManyInput<Classes, ClassesWhereInput> { }
