using CategoryManager.Category;
using CategoryManager.Model;
using CSharpFunctionalExtensions;

namespace CategoryManager.Repository.Interfaces;

public interface ICategoryRepository
{
	Result<CategorySummary> GetCategorySummaryById(int id);

	Result<ICategory> GetCategoryById(int id);

	bool AddObservation(Observation observation);

	List<string> GetSummary();
}
