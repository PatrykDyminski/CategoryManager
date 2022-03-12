using CategoryManager.Model;
using CSharpFunctionalExtensions;

namespace CategoryManager.CategoryDeterminer;

public interface ICategoryDeterminer
{
	Result<CategorySummary> DetermineCategory(Observation[] observations);
}
