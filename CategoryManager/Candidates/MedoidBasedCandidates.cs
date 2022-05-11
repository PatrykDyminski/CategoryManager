using CategoryManager.Macrostructure;
using CategoryManager.Utils;

namespace CategoryManager.Candidates;

public class MedoidBasedCandidates : ICandidatesExtractor
{
	private readonly IDistance macrostructure;

	public MedoidBasedCandidates(IDistance macrostructure)
	{
		this.macrostructure = macrostructure;
	}

	public int[][] ExtractCandidates(int[][] candidates, int[][] universe = null)
	{
		var dict = candidates
			.GroupBy(x => x, new ObservedObjectComparer())
			.Select(x => new { x.Key, Count = x.Count() })
			.ToDictionary(x => x.Key, x => x.Count);

		//Score each object
		var scoreDict = dict.Keys
			.Select(x => new
			{
				Key = x,
				Score = dict
					.Select(obj => obj.Value * macrostructure.CalculateDistance(obj.Key, x))
					.Sum()
			});

		//Find minimal score
		var minVal = scoreDict
			.MinBy(x => x.Score)!;

		//Return all values with minimal score
		return scoreDict
			.Where(x => x.Score == minVal.Score)
			.Select(y => y.Key)
			.ToArray();
	}
}
