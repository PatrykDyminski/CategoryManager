using System.Diagnostics.CodeAnalysis;

namespace CategoryManager.Utils;

public class IntArrayComparer : IEqualityComparer<int[]>
{
	public bool Equals(int[]? x, int[]? y)
	{
		return string.Equals(x.AsString(), y.AsString());
	}

	public int GetHashCode([DisallowNull] int[] obj)
	{
		return int.Parse(obj.AsString());
	}
}
