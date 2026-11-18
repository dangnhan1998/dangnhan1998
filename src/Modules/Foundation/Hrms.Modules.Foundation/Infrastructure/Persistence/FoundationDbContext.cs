using Hrms.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Hrms.Modules.Foundation.Infrastructure.Persistence;

internal sealed class FoundationDbContext(DbContextOptions<FoundationDbContext> options) : ModuleDbContext(options)
{
    protected override string Schema => FoundationModule.SchemaName;
}
