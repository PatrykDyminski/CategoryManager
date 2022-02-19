using CategoryManager.Candidates;
using CategoryManager.Categories;
using CategoryManager.Distance;
using CategoryManager.Model;

var hamming = new HammingDistance();
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

var medoidCandidated = new MedoidBasedCandidates();
var candidates = medoidCandidated.ExtractCandidates(list, hamming);

//Console.WriteLine(string.Join("", candidates[0]));

var cands2 = new CentroidBasedCandidates();
var candidates2 = cands2.ExtractCandidates(list, hamming, universe);

//Console.WriteLine(string.Join("", candidates2[0]));
//Console.WriteLine(string.Join("", candidates2[1]));


var observations = new Observation[]
{
	//related
	//1
	new Observation{ CategoryId = 1, IsRelated = true, ObservedObject = new int[] {0,0,0,0 } },

	//3
	new Observation{ CategoryId = 1, IsRelated = true, ObservedObject = new int[] {0,0,1,0 } },

	//4
	new Observation{ CategoryId = 1, IsRelated = true, ObservedObject = new int[] {0,0,1,1 } },
	new Observation{ CategoryId = 1, IsRelated = true, ObservedObject = new int[] {0,0,1,1 } },

	//6
	new Observation{ CategoryId = 1, IsRelated = true, ObservedObject = new int[] {0,1,0,1 } },
	new Observation{ CategoryId = 1, IsRelated = true, ObservedObject = new int[] {0,1,0,1 } },

	//not related
	//6
	new Observation{ CategoryId = 1, IsRelated = false, ObservedObject = new int[] {0,1,0,1 } },

	//10
	new Observation{ CategoryId = 1, IsRelated = false, ObservedObject = new int[] {1,0,0,1 } },
	new Observation{ CategoryId = 1, IsRelated = false, ObservedObject = new int[] {1,0,0,1 } },

	//13
	new Observation{ CategoryId = 1, IsRelated = false, ObservedObject = new int[] {1,1,0,0 } },

	//14
	new Observation{ CategoryId = 1, IsRelated = false, ObservedObject = new int[] {1,1,0,1 } },
	new Observation{ CategoryId = 1, IsRelated = false, ObservedObject = new int[] {1,1,0,1 } },
	new Observation{ CategoryId = 1, IsRelated = false, ObservedObject = new int[] {1,1,0,1 } },
};

var determier = new BasicCategoryDeterminer();

var category = determier.DetermineCategory(observations, hamming, medoidCandidated);

Console.WriteLine(category.Tminus);
Console.WriteLine(category.Tplus);
Console.WriteLine(string.Join("", category.Prototype));



