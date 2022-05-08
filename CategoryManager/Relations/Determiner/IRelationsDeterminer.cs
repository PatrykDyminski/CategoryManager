using CategoryManager.Category;
using CategoryManager.Relations.Types;
using CSharpFunctionalExtensions;

namespace CategoryManager.Relations.Determiner;

public interface IRelationsDeterminer
{
  Maybe<SpecificationRelation> DetermineSpecification(ICategory c1, ICategory c2);

  Maybe<ISimilarityRelation> DetermineSimilarityBasedOnObservationsSets(ICategory c1, ICategory c2);

  List<IRelation> GetRelationsForCategories(ICategory c1, ICategory c2);
}
