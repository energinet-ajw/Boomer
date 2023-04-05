using System.Linq.Expressions;
using PetStore.Domain.Base;

namespace PetStore.Domain.MouseAggregate.Specifications;

public class SearchByMouseNameSpecification : Specification<Mouse>
{
    private readonly string _name;

    public SearchByMouseNameSpecification(string name)
    {
        _name = name;
    }

    public override Expression<Func<Mouse, bool>> ToExpression()
    {
        return mouse => mouse.Name.Contains(_name);
    }
}
