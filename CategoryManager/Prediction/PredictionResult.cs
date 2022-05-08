using CategoryManager.Category;
using CategoryManager.Relations.Types;
using CSharpFunctionalExtensions;

namespace CategoryManager.Prediction;

internal class PredictionResult
{
  public ICategory ClosestCategory { get; init; }

  public bool IsInCore { get; init; }

  public Maybe<List<IRelation>> Relations { get; init; }
}
