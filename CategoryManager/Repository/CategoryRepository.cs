using CategoryManager.CategoryDeterminer;
using CategoryManager.Model;

namespace CategoryManager.Repository;

internal class CategoryRepository : ICategoryRepository
{
	private readonly ICategoryDeterminer CategoryDeterminer;


	private List<CategorySummary> Categories { get; }
	private Dictionary<int, List<Observation>> Observations { get; }

	public CategoryRepository(ICategoryDeterminer categoryDeterminer)
	{
		CategoryDeterminer = categoryDeterminer;

		Categories = new List<CategorySummary>();
		Observations = new Dictionary<int, List<Observation>>();
	}

	public void AddCategory(CategorySummary category)
	{
		Categories.Add(category);
	}

	public void AddObservation(Observation observation)
	{
		Observations[observation.CategoryId].Add(observation);
	}

	public void DisplaySummary()
	{
		throw new NotImplementedException();
	}
}
