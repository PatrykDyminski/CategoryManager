using CategoryManager.Category;
using CategoryManager.Relations.Features;
using CategoryManager.Relations.Types;
using CSharpFunctionalExtensions;

namespace CategoryManager.Relations.Determiner;

public class RelationsDeterminer : IRelationsDeterminer
{
  private readonly IRelationFeaturesDeterminer rfd;

  public RelationsDeterminer(IRelationFeaturesDeterminer rfd)
  {
    this.rfd = rfd;
  }

  public Maybe<SpecificationRelation> DetermineSpecification(ICategory c1, ICategory c2)
  {
    var op1 = rfd.CoreInsideCore(c1.Summary.Value, c2.Summary.Value, Maybe.None);
    var op2 = rfd.BoundaryInsideBoundary(c1.Summary.Value, c2.Summary.Value, Maybe.None);

    if (op1.HasValue && op2.HasValue && op1.Value.bigger == op2.Value.bigger)
    {
      return new SpecificationRelation
      {
        Cat1Id = c1.Id, 
        Cat2Id = c2.Id,
        BiggerCategoryId = op1.Value.bigger
      };
    }

    return Maybe.None;
  }

  public List<IRelation> GetRelationsForCategories(ICategory c1, ICategory c2)
  {
    //TODO Implemet this when more relations added
    var spec = DetermineSpecification(c1, c2);

    var rels = new List<IRelation>();

    if (spec.HasValue)
    {
      rels.Add(spec.GetValueOrDefault());
    }

    return rels;
  }
}
