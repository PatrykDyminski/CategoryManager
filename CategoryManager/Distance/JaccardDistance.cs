namespace CategoryManager.Distance;

public sealed class JaccardDistance : IDistance
{
	public double CalculateDistance(int[] object1, int[] object2)
	{
    if (object1.Length != object2.Length)
    {
      throw new ArgumentException("Objects must be equal length");
    }

    int inter = 0;
    int union = 0;

    for (int i = 0; i < object1.Length; i++)
    {
      if (object1[i] == 1 || object2[i] == 1)
      { 
        if (object1[i] == object2[i])
          inter++;
        union++;
      }
    }

    return (union == 0)
      ? 0
      : inter / (double)union;
  }
}
