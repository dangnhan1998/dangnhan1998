using Hrms.Infrastructure.Modules;
using Hrms.Modules.CnB;
using Hrms.Modules.Foundation;
using Hrms.Modules.Personnel;
using Hrms.Modules.Recruitment;
using Hrms.Modules.Training;

namespace Hrms.UnitTests;

public class ModuleRegistrationTests
{
    public static TheoryData<IModule> Modules =>
    [
        new FoundationModule(),
        new RecruitmentModule(),
        new PersonnelModule(),
        new TrainingModule(),
        new CnBModule(),
    ];

    [Theory]
    [MemberData(nameof(Modules))]
    public void Module_key_is_lowercase_and_not_empty(IModule module)
    {
        Assert.False(string.IsNullOrWhiteSpace(module.Key));
        Assert.Equal(module.Key.ToLowerInvariant(), module.Key);
    }
}
