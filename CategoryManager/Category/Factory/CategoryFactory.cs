using CategoryManager.CategoryDeterminer;
using CategoryManager.Distance;

namespace CategoryManager.Category.Factory;

public class CategoryFactory : ICategoryFactory
{
	private readonly IDistance distance;
	private readonly ICategoryDeterminer categoryDeterminer;

	public CategoryFactory(IDistance distance, ICategoryDeterminer categoryDeterminer)
	{
		this.distance = distance;
		this.categoryDeterminer = categoryDeterminer;
	}

	public Category CreateCategory(int categoryId)
	{
		return new Category(categoryDeterminer, distance, categoryId);
	}
}
