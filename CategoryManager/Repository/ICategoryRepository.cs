using CategoryManager.Model;

namespace CategoryManager.Repository;

internal interface ICategoryRepository
{
	void AddCategory(Category category);

	void AddObservation(Observation observation);

	void DisplaySummary();
}
