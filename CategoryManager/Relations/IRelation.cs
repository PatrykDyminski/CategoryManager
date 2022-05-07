using CSharpFunctionalExtensions;

namespace CategoryManager.Relations;

public abstract class IRelation
{
  public int Cat1Id { get; init; }
  public int Cat2Id { get; init; }
  public Maybe<int> BiggerCategoryId { get; init; }
}

public abstract class ISimilarityRelation : IRelation
{
  public SimilarityLevel SimilarityLevel { get; init; }
}

public enum SimilarityLevel
{
  Strong,
  Weak
}
