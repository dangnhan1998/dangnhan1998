using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hrms.Infrastructure.Persistence;

public static class PersistenceExtensions
{
    public const string ConnectionStringName = "Hrms";

    /// <summary>
    /// Đăng ký DbContext của một module trên database chung, bảng lịch sử migration nằm trong schema của module.
    /// </summary>
    public static IServiceCollection AddModuleDbContext<TContext>(
        this IServiceCollection services,
        IConfiguration configuration,
        string schema)
        where TContext : ModuleDbContext
    {
        var connectionString = configuration.GetConnectionString(ConnectionStringName);

        services.AddDbContext<TContext>(options =>
            options.UseSqlServer(connectionString, sql =>
                sql.MigrationsHistoryTable("__EFMigrationsHistory", schema)));

        return services;
    }
}
