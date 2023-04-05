using PetStore.Domain.MouseAggregate;
using PetStore.Domain.MouseAggregate.Specifications;
using Xunit;
using FluentAssertions;

namespace PetStore.Tests.Domain.MouseAggregate.Specifications;

public class SearchByMouseNameSpecificationTests
{
    [Fact]
    public void ToExpression_HasTheCorrectStringValue()
    {
        // Arrange
        var sut = new SearchByMouseNameSpecification("name");
        var expected = $"mouse => mouse.Name.Contains(value({typeof(SearchByMouseNameSpecification).FullName})._name)";

        // Act
        var actual = sut.ToExpression();

        // Assert
        Assert.Equal(expected, actual.ToString());
        //  Assert.True(ExpressionEqualityComparer.Instance.Equals(expected, actual));
    }

    [Fact]
    public void IsSatisfiedBy_OnBaseClass_FiltersAListOfMiceBasedOnName()
    {
        // Arrange
        const string searchName = "lly";
        var mouse1 = new Mouse("jolly1");
        var mouse2 = new Mouse("jolly2");
        var mouse3 = new Mouse("jolly3");
        var mouse4 = new Mouse("jolly4");
        var mouse5 = new Mouse("doe");

        var mice = new[] { mouse1, mouse2, mouse3, mouse4, mouse5 }.AsQueryable();
        var expected = new[] { mouse1, mouse2, mouse3, mouse4 }.AsQueryable();
        var sut = new SearchByMouseNameSpecification(searchName);

        // Act
        var actual = mice.AsEnumerable().Where(sut.IsSatisfiedBy);

        // Assert
        actual.Should().BeEquivalentTo(expected);
    }
}