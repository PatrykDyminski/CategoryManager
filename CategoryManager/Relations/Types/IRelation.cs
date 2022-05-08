using CSharpFunctionalExtensions;

namespace CategoryManager.Relations.Types;

public abstract class IRelation
{
  public int Cat1Id { get; init; }
  public int Cat2Id { get; init; }
  public Maybe<int> BiggerCategoryId { get; init; }

  public override string ToString()
  {
    return $"RelationType: {nameof(IRelation)}, Cat1 Id: {Cat1Id}, Cat2 Id: {Cat2Id}";
  }
}

public abstract class ISimilarityRelation : IRelation
{
  public SimilarityLevel SimilarityLevel { get; init; }

  public override string ToString()
  {
    return $"RelationType: {nameof(ISimilarityRelation)}, Level: {SimilarityLevel}, Cat1 Id: {Cat1Id}, Cat2 Id: {Cat2Id}";
  }
}

public enum SimilarityLevel
{
  Strong,
  Weak
}
