using CategoryManager.Distance;

namespace CategoryManager.Candidates;

public interface ICandidatesExtractor
{
	int[][] ExtractCandidates(int[][] candidates, IDistance macrostructure, int[][] universe = null);
}
