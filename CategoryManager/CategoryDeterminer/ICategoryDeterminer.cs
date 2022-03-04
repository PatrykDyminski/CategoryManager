using CategoryManager.Model;

namespace CategoryManager.CategoryDeterminer;

public interface ICategoryDeterminer
{
	CategorySummary DetermineCategory(Observation[] observations);
}
