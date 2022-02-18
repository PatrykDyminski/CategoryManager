namespace CategoryManager.Distance;

internal sealed class HammingDistance : IDistance
{
  public double CalculateDistance(int[] object1, int[] object2)
  {
    if (object1.Length != object2.Length)
    {
      throw new Exception("Objects must be equal length");
    }

    int distance = object1
      .Zip(object2, (c1, c2) => new { c1, c2 })
      .Count(m => m.c1 != m.c2);

    return distance;
  }
}
