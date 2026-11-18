using Hrms.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Hrms.Modules.CnB.Infrastructure.Persistence;

internal sealed class CnBDbContext(DbContextOptions<CnBDbContext> options) : ModuleDbContext(options)
{
    protected override string Schema => CnBModule.SchemaName;
}
