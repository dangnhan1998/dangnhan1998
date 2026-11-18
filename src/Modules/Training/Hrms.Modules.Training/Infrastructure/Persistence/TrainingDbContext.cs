using Hrms.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Hrms.Modules.Training.Infrastructure.Persistence;

internal sealed class TrainingDbContext(DbContextOptions<TrainingDbContext> options) : ModuleDbContext(options)
{
    protected override string Schema => TrainingModule.SchemaName;
}
