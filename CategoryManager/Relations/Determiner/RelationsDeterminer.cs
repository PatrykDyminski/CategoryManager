using CategoryManager.Category;
using CategoryManager.Relations.Features;
using CategoryManager.Relations.Types;
using CategoryManager.Utils;
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
        BiggerCategoryId = op1.Value.bigger == 1 
          ? c1.Id 
          : c2.Id
      };
    }

    return Maybe.None;
  }

  public Maybe<ISimilarityRelation> DetermineSimilarityBasedOnObservationsSets(ICategory c1, ICategory c2)
  {
    var weakRatio = 0.35;
    var strongRatio = 0.9;

    var c1obs = c1.GetCoreObservations();
    var c2obs = c2.GetCoreObservations();

    var c1boundObs = c1.GetPositiveBoundaryObservations();
    var c2boundObs = c2.GetPositiveBoundaryObservations();

    if (c1obs.HasValue && c2obs.HasValue)
    {
      var cir = rfd.IntersectionRatio(c1obs.Value, c2obs.Value);

      if (cir >= weakRatio)
      {
        //Happended once that boundary was empty TODO
        if(c1boundObs.HasNoValue || c2boundObs.HasNoValue)
        {
          return Maybe<ISimilarityRelation>.None;
        }

        var bir = rfd.IntersectionRatio(c1boundObs.Value, c2boundObs.Value);

        //Strong Similarity
        if (cir >= strongRatio && bir >= strongRatio && rfd.PrototypesInsideCores(c1, c2))
        {
          return new ObservationBasedSimilarity
          {
            Cat1Id = c1.Id,
            Cat2Id = c2.Id,
            SimilarityLevel = SimilarityLevel.Weak
          };
        }
        //Weak Similarity
        else if (bir >= weakRatio)
        {
          return new ObservationBasedSimilarity
          {
            Cat1Id = c1.Id,
            Cat2Id = c2.Id,
            SimilarityLevel = SimilarityLevel.Weak
          };
        }
      }
    }

    return Maybe<ISimilarityRelation>.None;
  }

  public List<IRelation> GetRelationsForCategories(ICategory c1, ICategory c2)
  {
    var rels = new List<IRelation>();

    var spec = DetermineSpecification(c1, c2);
    var similarity = DetermineSimilarityBasedOnObservationsSets(c1, c2);

    return rels
      .AddIfHasValue(similarity)
      .AddIfHasValue(spec);
  }
}
