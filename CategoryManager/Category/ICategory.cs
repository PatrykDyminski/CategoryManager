using CategoryManager.Model;

namespace CategoryManager.Category;

public interface ICategory
{
	void AddObservation(Observation observation);

	CategorySummary Summary { get; }

	void DisplayCategorySummary();
}
