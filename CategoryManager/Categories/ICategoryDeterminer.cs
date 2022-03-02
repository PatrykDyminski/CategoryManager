using CategoryManager.Candidates;
using CategoryManager.Distance;
using CategoryManager.Model;

namespace CategoryManager.Categories;

public interface ICategoryDeterminer
{
	Category DetermineCategory(Observation[] observations);
}
