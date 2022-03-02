namespace CategoryManager.Candidates;

public interface ICandidatesExtractor
{
	int[][] ExtractCandidates(int[][] candidates, int[][] universe = null);
}
