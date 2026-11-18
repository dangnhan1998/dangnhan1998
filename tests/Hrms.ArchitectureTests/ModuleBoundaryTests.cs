using System.Reflection;

namespace Hrms.ArchitectureTests;

public class ModuleBoundaryTests
{
    private const string ModulePrefix = "Hrms.Modules.";
    private const string ContractsSuffix = ".Contracts";

    private static readonly string[] ModuleNames = ["Foundation", "Recruitment", "Personnel", "Training", "CnB"];

    public static TheoryData<string> Modules => new(ModuleNames);

    [Theory]
    [MemberData(nameof(Modules))]
    public void Module_only_references_other_modules_through_contracts(string module)
    {
        var assembly = Assembly.Load($"{ModulePrefix}{module}");

        var forbidden = assembly.GetReferencedAssemblies()
            .Select(a => a.Name!)
            .Where(name => name.StartsWith(ModulePrefix, StringComparison.Ordinal))
            .Where(name => !name.EndsWith(ContractsSuffix, StringComparison.Ordinal))
            .Where(name => name != $"{ModulePrefix}{module}")
            .ToList();

        Assert.Empty(forbidden);
    }

    [Theory]
    [MemberData(nameof(Modules))]
    public void Contracts_do_not_reference_module_implementations(string module)
    {
        var assembly = Assembly.Load($"{ModulePrefix}{module}{ContractsSuffix}");

        var forbidden = assembly.GetReferencedAssemblies()
            .Select(a => a.Name!)
            .Where(name => name.StartsWith(ModulePrefix, StringComparison.Ordinal))
            .Where(name => !name.EndsWith(ContractsSuffix, StringComparison.Ordinal))
            .ToList();

        Assert.Empty(forbidden);
    }
}
