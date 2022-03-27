using CategoryManager.Model;
using CSharpFunctionalExtensions;

namespace CategoryManager.CategoryDeterminer;

public interface ICategoryDeterminer
{
	Result<CategoryDeterminationResultDTO> DetermineCategory(Observation[] observations);
}
