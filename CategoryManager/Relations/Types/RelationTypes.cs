namespace CategoryManager.Relations.Types;

public class SpecificationRelation : IRelation
{
  public override string ToString()
  {
    return $"RelationType: {nameof(SpecificationRelation)}, Cat1 Id: {Cat1Id}, Cat2 Id: {Cat2Id}";
  }
}

public class ObservationBasedSimilarity : ISimilarityRelation
{
  public override string ToString()
  {
    return $"RelationType: {nameof(ObservationBasedSimilarity)}, Level: {SimilarityLevel}, Cat1 Id: {Cat1Id}, Cat2 Id: {Cat2Id}";
  }
}