using CategoryManager.Model;
using CSharpFunctionalExtensions;

namespace CategoryManager.Repository.Interfaces;

internal interface ICategoryRepository
{
	Result<CategorySummary> GetCategorySummaryById(int id);

	bool AddObservation(Observation observation);

	void DisplaySummary();
}
