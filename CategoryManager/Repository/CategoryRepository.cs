using CategoryManager.Category;
using CategoryManager.Category.Factory;
using CategoryManager.Model;
using CategoryManager.Repository.Interfaces;
using CSharpFunctionalExtensions;

namespace CategoryManager.Repository;

public class CategoryRepository : ICategoryRepository
{
	private readonly ICategoryFactory categoryFactory;

	private List<ICategory> Categories { get; }

	public CategoryRepository(ICategoryFactory categoryFactory)
	{
		this.categoryFactory = categoryFactory;

		Categories = new List<ICategory>();
	}

	public bool AddObservation(Observation observation)
	{
		//if there is no such category observed before
		EnsureCategoryExist(observation);

		return Categories
			.Single(x => x.Id == observation.CategoryId)
			.AddObservation(observation);
	}

	public Result<CategorySummary> GetCategorySummaryById(int id)
	{
		var cat = Categories
			.SingleOrDefault(x => x.Id == id);

		return cat != null
			? cat.Summary.ToResult("Category exist but has no summary yet")
			: Result.Failure<CategorySummary>("No such category");
	}

	public Result<ICategory> GetCategoryById(int id)
	{
		var cat = Categories
			.SingleOrDefault(x => x.Id == id);

		return cat != null
			? Result.Success(cat)
			: Result.Failure<ICategory>("No such category");
	}

	public void DisplaySummary()
	{
		Categories.ForEach(x => x.DisplayCategorySummary());
	}

	private void EnsureCategoryExist(Observation observation)
	{
		if (!Categories.Where(x => x.Id == observation.CategoryId).Any())
		{
			Categories.Add(categoryFactory.CreateCategory(observation.CategoryId));
		}
	}
}
