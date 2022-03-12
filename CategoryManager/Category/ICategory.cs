using CategoryManager.Model;
using CSharpFunctionalExtensions;

namespace CategoryManager.Category;

public interface ICategory
{
	void AddObservation(Observation observation);

	int Id { get; }

	Maybe<CategorySummary> Summary { get; }

	void DisplayCategorySummary();
}
