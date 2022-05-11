using CategoryManager.Macrostructure;
using CategoryManager.Repository.Interfaces;
using CSharpFunctionalExtensions;

namespace CategoryManager.Prediction;

internal class CategoryPredictor : ICategoryPredictor
{
  private readonly ICategoryRepository categoryRepository;
  private readonly IRelationsRepository relationsRepository;
  private readonly IDistance macrostructure;

  public CategoryPredictor(ICategoryRepository categoryRepository, IRelationsRepository relationsRepository, IDistance macrostructure)
  {
    this.categoryRepository = categoryRepository;
    this.relationsRepository = relationsRepository;
    this.macrostructure = macrostructure;
  }

  public Result<PredictionResult> PredictCategory(int[] objectoToPredict, bool includeRelations)
  {
    var cat = categoryRepository.GetAllCategories()
      .Where(cat => cat.Summary.HasValue)
      .Select(category => new
      {
        Category = category,
        Distance = macrostructure.CalculateDistance(category.Summary.Value.Prototype, objectoToPredict)
      })
      .MinBy(category => category.Distance);

    if(cat != null)
    {
      if (includeRelations)
      {
        return new PredictionResult
        {
          ClosestCategory = cat.Category,
          IsInCore = cat.Distance <= cat.Category.Summary.Value.Tplus,
          Relations = relationsRepository
            .GetRelationsForCategory(cat.Category.Id)
            .ToList()
        };
      }

      return new PredictionResult
      {
        ClosestCategory = cat.Category,
        IsInCore = cat.Distance <= cat.Category.Summary.Value.Tplus
      };
    }
    else
    {
      return Result.Failure<PredictionResult>("No");
    }
  }
}
