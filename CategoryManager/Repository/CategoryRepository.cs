using CategoryManager.Category.Factory;
using CategoryManager.Model;
using CategoryManager.Repository.Interfaces;
using CSharpFunctionalExtensions;

namespace CategoryManager.Repository;

internal class CategoryRepository : ICategoryRepository
{
	private readonly ICategoryFactory categoryFactory;

	private List<Category.Category> Categories { get; }

	public CategoryRepository(ICategoryFactory categoryFactory)
	{
		this.categoryFactory = categoryFactory;

		Categories = new List<Category.Category>();
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
			Categories.Add(categoryFactory.CreateCategory(observation.CategoryId));
		}
	}

	public Result<CategorySummary> GetCategorySummaryById(int id)
	{
		var cat = Categories
			.SingleOrDefault(x => x.Id == id);

		return cat != null
			? cat.Summary.ToResult("Category exist but has no summary yet")
			: Result.Failure<CategorySummary>("No such category");
	}
	public void DisplaySummary()
	{
		Categories.ForEach(x => x.DisplayCategorySummary());
	}
}
