using Hrms.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Hrms.Modules.Recruitment.Infrastructure.Persistence;

internal sealed class RecruitmentDbContext(DbContextOptions<RecruitmentDbContext> options) : ModuleDbContext(options)
{
    protected override string Schema => RecruitmentModule.SchemaName;
}
