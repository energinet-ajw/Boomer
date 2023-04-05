using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;

namespace PetStore.Tests.Base;

public class AutoMoqDataAttribute : AutoDataAttribute
{
    public AutoMoqDataAttribute()
        : base(() =>
        {
            var fixture = new Fixture();
            fixture.Customize(
                new CompositeCustomization(
                    new AutoMoqCustomization()));

            fixture.Behaviors.Add(new OmitOnRecursionBehavior(1));

            return fixture;
        })
    {
    }
}