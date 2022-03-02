using CategoryManager.Distance;
using CategoryManager.Utils;

namespace CategoryManager.Candidates;

public sealed class CentroidBasedCandidates : ICandidatesExtractor
{
	private readonly IDistance macrostructure;

	public CentroidBasedCandidates(IDistance macrostructure)
	{
		this.macrostructure = macrostructure;
	}

	public int[][] ExtractCandidates(int[][] candidates, int[][] universe)
	{
		var dict = candidates
			.GroupBy(x => x, new IntArrayComparer())
			.Select(x => new { x.Key, Count = x.Count() })
			.ToDictionary(x => x.Key, x => x.Count);

		//Score each object in universe
		var scoreDict = universe
			.Select(x => new
			{
				Key = x,
				Score = dict.Select(obj => obj.Value * macrostructure.CalculateDistance(obj.Key, x)).Sum()
			});

		//Find minimal score
		var minVal = scoreDict.MinBy(x => x.Score)!;

		//Return all values with minimal score
		return scoreDict.Where(x => x.Score == minVal.Score).Select(y => y.Key).ToArray();
	}
}
