using CategoryManager.Distance;
using System.Diagnostics.CodeAnalysis;

namespace CategoryManager.Candidates;

internal class MedoidBasedCandidates : IExtractCandidates
{
	public int[][] ExtractCandidates(int[][] candidates, IDistance distance)
	{
		var dict = candidates
			.GroupBy(x => x, new IntArrayComparer())
			.Select(x => new { x.Key, Count = x.Count() })
			.ToDictionary(x => x.Key, x => x.Count);

		//dict.Select(i => $"{string.Join("", i.Key)}: {i.Value}")
		//	.ToList()
		//	.ForEach(Console.WriteLine);

		//Score each object
		var scoreDict = dict.Keys
			.Select(x => new
			{
				x,
				Score = dict.Select(obj => obj.Value * distance.CalculateDistance(obj.Key, x)).Sum()
			});

		//Find minimal score
		var minVal = scoreDict.MinBy(x => x.Score)!;

		//Return all values with minimal score
		return scoreDict.Where(x => x.Score == minVal.Score).Select(y => y.x).ToArray();
	}
}

public class IntArrayComparer : IEqualityComparer<int[]>
{
	public bool Equals(int[]? x, int[]? y)
	{
		return string.Equals(string.Join("", x),string.Join("", y));
	}

	public int GetHashCode([DisallowNull] int[] obj)
	{
		var str = string.Join("", obj);
		return int.Parse(str);
	}
}
