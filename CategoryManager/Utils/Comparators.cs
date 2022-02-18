using System.Diagnostics.CodeAnalysis;

namespace CategoryManager.Utils;

public class IntArrayComparer : IEqualityComparer<int[]>
{
	public bool Equals(int[]? x, int[]? y)
	{
		return string.Equals(string.Join("", x), string.Join("", y));
	}

	public int GetHashCode([DisallowNull] int[] obj)
	{
		var str = string.Join("", obj);
		return int.Parse(str);
	}
}
