using CategoryManager.Model;

namespace CategoryManager.Category;

public interface ICategory
{
	void AddObservation(Observation observation);

	int Id { get; }

	CategorySummary Summary { get; }

	void DisplayCategorySummary();
}
