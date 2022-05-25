using CategoryManager.Category.Recalculation;
using CategoryManager.CategoryDeterminer;
using CategoryManager.Macrostructure;

namespace CategoryManager.Category.Factory;

public class CategoryFactory : ICategoryFactory
{
	private readonly IDistance distance;
	private readonly ICategoryDeterminer categoryDeterminer;
  private readonly ICategoryRecalculationDeterminer crd;

  public CategoryFactory(
		IDistance distance, 
		ICategoryDeterminer categoryDeterminer, 
		ICategoryRecalculationDeterminer crd)
	{
		this.distance = distance;
		this.categoryDeterminer = categoryDeterminer;
    this.crd = crd;
  }

	public Category CreateCategory(int categoryId)
	{
		return new Category(categoryDeterminer, distance, crd, categoryId);
	}
}
