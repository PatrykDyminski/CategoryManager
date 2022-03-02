using CategoryManager.Categories;
using CategoryManager.Model;

namespace CategoryManager.Repository;

internal class CategoryRepository : ICategoryRepository
{
	private readonly ICategoryDeterminer CategoryDeterminer;


	private List<Category> Categories { get; }
	private Dictionary<int, List<Observation>> Observations { get; }

	public CategoryRepository(ICategoryDeterminer categoryDeterminer)
	{
		CategoryDeterminer = categoryDeterminer;

		Categories = new List<Category>();
		Observations = new Dictionary<int, List<Observation>>();
	}

	public void AddCategory(Category category)
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

	private void RecalculateCategory(int categoryId)
	{
		
	}
}
