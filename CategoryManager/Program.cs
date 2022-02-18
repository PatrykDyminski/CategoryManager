using CategoryManager.Candidates;
using CategoryManager.Distance;

var dst = new HammingDistance();
var dst2 = new JaccardDistance();

int[] obj1 = { 0, 1, 0, 1, 0, 1 };
int[] obj2 = { 0, 0, 0, 1, 1, 1 };

//Console.WriteLine(dst.CalculateDistance(obj1, obj2));
//Console.WriteLine(dst2.CalculateDistance(obj1, obj2));

var universe = new int[][]
{
	new int[] { 0, 0, 0},
	new int[] { 0, 0, 1},
	new int[] { 0, 1, 0},
	new int[] { 0, 1, 1},
	new int[] { 1, 0, 0},
	new int[] { 1, 0, 1},
	new int[] { 1, 1, 0},
	new int[] { 1, 1, 1},
};

var list = new int[][]
{
	new int[] { 0, 0, 0},
	new int[] { 0, 1, 0},
	new int[] { 0, 1, 1},
	new int[] { 0, 1, 1},
	new int[] { 1, 0, 1},
	new int[] { 1, 0, 1},
};

var cands = new MedoidBasedCandidates();
var candidates = cands.ExtractCandidates(list, dst);

Console.WriteLine(string.Join("", candidates[0]));

var cands2 = new CentroidBasedCandidates();
var candidates2 = cands2.ExtractCandidates(list, dst, universe);

Console.WriteLine(string.Join("", candidates2[0]));
Console.WriteLine(string.Join("", candidates2[1]));
