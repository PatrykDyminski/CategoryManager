using CategoryManager.Model;
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

public class ObservationArrayComparer : IEqualityComparer<Observation>
{
	public bool Equals(Observation? x, Observation? y)
	{
		return string.Equals(x.ObservedObject.AsString(), y.ObservedObject.AsString());
	}

	public int GetHashCode([DisallowNull] Observation obj)
	{
		return int.Parse(obj.ObservedObject.AsString());
	}
}
