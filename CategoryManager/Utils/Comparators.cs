using CategoryManager.Model;
using System.Diagnostics.CodeAnalysis;

namespace CategoryManager.Utils;

public class ObservedObjectComparer : IEqualityComparer<int[]>
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

public class ObservationComparer : IEqualityComparer<Observation>
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

public class ObservationComparerWithRelation : IEqualityComparer<Observation>
{
	public bool Equals(Observation? x, Observation? y)
	{
		return string.Equals(x.ObservedObject.AsString(), y.ObservedObject.AsString()) && x.IsRelated == y.IsRelated;
	}

	public int GetHashCode([DisallowNull] Observation obj)
	{
		var str = obj.IsRelated
			? "1"
			: "0";

		return int.Parse(obj.ObservedObject.AsString() + str);
	}
}
