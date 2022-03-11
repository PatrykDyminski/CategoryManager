using CategoryManager.Model;

namespace CategoryManager.CategoryDeterminer;

public interface ICategoryDeterminer
{
	(bool isSuccess, CategorySummary categorySummary) DetermineCategory(Observation[] observations);
}
