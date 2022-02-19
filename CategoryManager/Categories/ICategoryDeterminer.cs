using CategoryManager.Candidates;
using CategoryManager.Distance;
using CategoryManager.Model;

namespace CategoryManager.Categories;

internal interface ICategoryDeterminer
{
	Category DetermineCategory(Observation[] observations, IDistance macrostructure, ICandidatesExtractor candidatesExtractor);
}
