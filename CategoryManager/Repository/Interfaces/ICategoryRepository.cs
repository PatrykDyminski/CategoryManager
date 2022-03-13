using CategoryManager.Model;

namespace CategoryManager.Repository.Interfaces;

internal interface ICategoryRepository
{
	bool AddObservation(Observation observation);

	void DisplaySummary();
}
