using CategoryManager.Relations;
using CSharpFunctionalExtensions;

namespace CategoryManager.Utils;

public static class ExtensionMethods
{
  public static string AsString(this int[] array)
  {
    return string.Join("", array);
  }

  public static string AsString(this Maybe<int[]> array)
  {
    return array.GetValueOrDefault(x => string.Join("", x), "Empty Array");
  }

  public static bool Involve(this IRelation relation, int categoryId)
  {
    return relation.Cat1Id == categoryId || relation.Cat2Id == categoryId;
  }
}
