using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Dsl;
using AutoFixture.Kernel;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit.Microsoft.DependencyInjection;
using Xunit.Microsoft.DependencyInjection.Abstracts;
using _1likte.Application;
using _1likte.Persistence;
using Microsoft.Extensions.Configuration;

namespace _1likte.UnitTests.Base;

/// <summary>
/// Testler için temel sınıftır.
/// </summary>
public class XUnitBase : TestBedFixture
{
    protected IConfiguration Configuration { get; private set; }


    public IFixture Fixture { get; protected set; } = CreateFixture();

    protected TPoco GeneratePoco<TPoco>(Func<ICustomizationComposer<TPoco>, IPostprocessComposer<TPoco>> customizationAction = null)
    {
        ICustomizationComposer<TPoco> a = Fixture.Build<TPoco>();
        if (customizationAction != null)
            return customizationAction(a).Create();

        return a.Create();
    }

    protected Mock<TService> GenerateMock<TService>(Func<ICustomizationComposer<Mock<TService>>, ISpecimenBuilder> composerTransformation = null)
      where TService : class
    {
        if (composerTransformation == null)
            return Fixture.Freeze<Mock<TService>>();
        else
            return Fixture.Freeze(composerTransformation);
    }

    public static IFixture CreateFixture()
    {
        var fixture = new Fixture().Customize(new AutoMoqCustomization { ConfigureMembers = true });

        // client has a circular reference from AutoFixture point of view
        fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
        fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        return fixture;
    }

    protected TService GenerateService<TService>(Action<ICustomizationComposer<TService>> customizationAction = null)
    {
        ICustomizationComposer<TService> a = Fixture.Build<TService>();
        customizationAction?.Invoke(a);
        return a.Create();
    }

    protected override IEnumerable<TestAppSettings> GetTestAppSettings()
    {
        yield return new() { Filename = "appsettings.json", IsOptional = true };
    }

    protected override ValueTask DisposeAsyncCore() => new();

    protected override void AddServices(IServiceCollection services, IConfiguration? configuration)
    {
        SetConfiguration();
        services.AddApplicationServices(configuration!);
        services.AddPersistenceServices(configuration!);
    }
    private void SetConfiguration()
    {
        //Configuration = new ConfigurationBuilder()
        //           .AddJsonFile("appsettings.json")
        //           .Build();
    }

}
