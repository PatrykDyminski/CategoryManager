namespace CategoryManager.Category.Recalculation;

public interface ICategoryRecalculationDeterminer
{
  bool ShouldRecalculate(int currentCountOfObjects, int countOfObjectsWhenRecalculated);
}
