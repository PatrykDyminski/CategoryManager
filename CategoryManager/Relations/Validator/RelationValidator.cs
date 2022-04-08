using CategoryManager.Relations.Determiner;
using CategoryManager.Relations.Types;
using CategoryManager.Repository.Interfaces;

namespace CategoryManager.Relations.Validator;

public class RelationValidator : IRelationValidator
{
  private readonly ICategoryRepository categoryRepository;
  private readonly IRelationsDeterminer relationsDeterminer;

  public RelationValidator(ICategoryRepository categoryRepository, IRelationsDeterminer relationsDeterminer)
  {
    this.categoryRepository = categoryRepository;
    this.relationsDeterminer = relationsDeterminer;
  }

  public bool ValidateRelation(IRelation relation)
  {
    var cat1 = categoryRepository.GetCategoryById(relation.Cat1Id);
    var cat2 = categoryRepository.GetCategoryById(relation.Cat2Id);

    if (cat1.IsFailure || cat2.IsFailure)
    {
      return false;
    }

    return relation switch
    {
      SpecificationRelation => relationsDeterminer.DetermineSpecification(cat1.Value, cat2.Value).HasValue,
      _ => throw new NotImplementedException(),
    };
  }
}
