using CategoryManager.Category;
using CategoryManager.Relations.Determiner;
using CategoryManager.Relations.Types;
using CategoryManager.Relations.Validator;
using CategoryManager.Repository.Interfaces;
using CategoryManager.Utils;
using CSharpFunctionalExtensions;

namespace CategoryManager.Repository;

public class RelationsRepository : IRelationsRepository
{
  private readonly IRelationsDeterminer relationsDeterminer;
  private readonly ICategoryRepository categoryRepository;
  private readonly IRelationValidator relationValidator;

  private List<IRelation> Relations;
  private List<int> KnownCategories;

  public RelationsRepository(
    IRelationsDeterminer relationsDeterminer,
    ICategoryRepository categoryRepository,
    IRelationValidator relationValidator)
  {
    this.relationsDeterminer = relationsDeterminer;
    this.categoryRepository = categoryRepository;
    this.relationValidator = relationValidator;

    Relations = new List<IRelation>();
    KnownCategories = new List<int>();
  }

  public IReadOnlyCollection<IRelation> GetAllRelations() => Relations;

  public IReadOnlyCollection<IRelation> GetRelationsForCategory(int categoryId)
  {
    return Relations
      .Where(r => r.Involve(categoryId))
      .ToList();
  }

  public void UpdateRelations(ICategory category)
  {
    if (IsNewCategory(category.Id))
    {
      HandleNewCategory(category);
    }
    else
    {
      HandleExistingRelationsForCategory(category);
    }
  }

  private void HandleExistingRelationsForCategory(ICategory category)
  {
    var validRelations = Relations
      .Where(relation => relation.Involve(category.Id))
      .Where(x => relationValidator.ValidateRelation(x));

    Relations = Relations
      .Where(relation => !relation.Involve(category.Id))
      .Concat(validRelations)
      .ToList();
  }

  private void HandleNewCategory(ICategory category)
  {
    var listOfRelations = categoryRepository.GetAllCategories()
      .Where(cat => cat.Id != category.Id)
      .Where(cat => cat.Summary.HasValue)
      .SelectMany(cat => relationsDeterminer
        .GetRelationsForCategories(category, cat));

    if (listOfRelations.Any())
    {
      Relations.AddRange(listOfRelations);
    }

    KnownCategories.Add(category.Id);
  }

  private bool IsNewCategory(int categoryId)
  {
    return !KnownCategories.Contains(categoryId);
  }
}
