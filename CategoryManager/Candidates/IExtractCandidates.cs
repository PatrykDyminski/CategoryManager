using CategoryManager.Distance;

namespace CategoryManager.Candidates;

internal interface IExtractCandidates
{
	int[][] ExtractCandidates(int[][] candidates, IDistance distance);
}
