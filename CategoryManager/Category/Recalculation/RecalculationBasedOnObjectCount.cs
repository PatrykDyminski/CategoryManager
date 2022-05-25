namespace CategoryManager.Category.Recalculation;

public class RecalculationBasedOnObjectCount : ICategoryRecalculationDeterminer
{
  private readonly int objectCountToInvoke;

  public RecalculationBasedOnObjectCount(int objectCountToInvoke)
  {
    this.objectCountToInvoke = objectCountToInvoke;

    Console.WriteLine(objectCountToInvoke);
  }

  public bool ShouldRecalculate(int currentCountOfObjects, int countOfObjectsWhenRecalculated)
  {
    return currentCountOfObjects - countOfObjectsWhenRecalculated >= objectCountToInvoke;
  }
}
