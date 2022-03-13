using CategoryManager.Model;
using CSharpFunctionalExtensions;

namespace CategoryManager.Category;

public interface ICategory
{
	//returns true if category has been recalculated
	bool AddObservation(Observation observation);

	int Id { get; }

	Maybe<CategorySummary> Summary { get; }

	void DisplayCategorySummary();
}
