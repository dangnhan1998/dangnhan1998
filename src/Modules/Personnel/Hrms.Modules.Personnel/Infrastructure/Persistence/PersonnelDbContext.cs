using Hrms.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Hrms.Modules.Personnel.Infrastructure.Persistence;

internal sealed class PersonnelDbContext(DbContextOptions<PersonnelDbContext> options) : ModuleDbContext(options)
{
    protected override string Schema => PersonnelModule.SchemaName;
}
