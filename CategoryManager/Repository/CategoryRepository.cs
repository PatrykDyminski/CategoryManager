using CategoryManager.CategoryDeterminer;
using CategoryManager.Model;
using CategoryManager.Repository.Interfaces;

namespace CategoryManager.Repository;

internal class CategoryRepository : ICategoryRepository
{
	private readonly ICategoryDeterminer categoryDeterminer;

	private List<Category.Category> Categories { get; }

	private readonly IRelationsRepository relationsRepository;

	public CategoryRepository(ICategoryDeterminer categoryDeterminer)
	{
		Categories = new List<Category.Category>();

		relationsRepository = new RelationsRepository();

		this.categoryDeterminer = categoryDeterminer;
	}

	public bool AddObservation(Observation observation)
	{
		//if there is no such category observed before
		EnsureCategoryExist(observation);

		return Categories
			.Single(x => x.Id == observation.CategoryId)
			.AddObservation(observation);
	}

	private void EnsureCategoryExist(Observation observation)
	{
		if (!Categories.Where(x => x.Id == observation.CategoryId).Any())
		{
			Categories.Add(new Category.Category(categoryDeterminer, observation.CategoryId));
		}
	}

	public void DisplaySummary()
	{
		Categories.ForEach(x => x.DisplayCategorySummary());
	}
}
