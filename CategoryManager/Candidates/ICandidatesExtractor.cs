using CategoryManager.Distance;

namespace CategoryManager.Candidates;

internal interface ICandidatesExtractor
{
	int[][] ExtractCandidates(int[][] candidates, IDistance macrostructure, int[][] universe = null);
}
