using CategoryManager.Model;
using CSharpFunctionalExtensions;
using System.Collections.Immutable;

namespace CategoryManager.Category;

public interface ICategory
{
	//returns true if category has been recalculated
	bool AddObservation(Observation observation);

	int Id { get; }

	Maybe<CategorySummary> Summary { get; }

	Maybe<ImmutableHashSet<Observation>> GetCoreObservations();

	Maybe<ImmutableHashSet<Observation>> GetPositiveBoundaryObservations();

	void DisplayCategorySummary();
}
