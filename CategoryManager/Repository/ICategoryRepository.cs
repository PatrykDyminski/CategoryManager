using CategoryManager.Model;

namespace CategoryManager.Repository;

internal interface ICategoryRepository
{
	void AddCategory(CategorySummary category);

	void AddObservation(Observation observation);

	void DisplaySummary();
}
