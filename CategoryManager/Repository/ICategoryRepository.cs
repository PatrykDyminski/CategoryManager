using CategoryManager.Model;

namespace CategoryManager.Repository;

internal interface ICategoryRepository
{
	void AddObservation(Observation observation);

	void DisplaySummary();
}
