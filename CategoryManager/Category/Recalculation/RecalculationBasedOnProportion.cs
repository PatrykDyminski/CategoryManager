namespace CategoryManager.Category.Recalculation;

public class RecalculationBasedOnProportion : ICategoryRecalculationDeterminer
{
  private readonly float proportion;

  public RecalculationBasedOnProportion(float proportion)
  {
    this.proportion = proportion;
  }

  public bool ShouldRecalculate(int currentCountOfObjects, int countOfObjectsWhenRecalculated)
  {
    int newCount = currentCountOfObjects - countOfObjectsWhenRecalculated;

    return newCount >= countOfObjectsWhenRecalculated * proportion;
  }
}
