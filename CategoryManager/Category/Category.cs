using CategoryManager.CategoryDeterminer;
using CategoryManager.Model;

namespace CategoryManager.Category;

public class Category : ICategory
{
	private readonly ICategoryDeterminer categoryDeterminer;

	private readonly int categoryId;
	private readonly List<Observation> observations;
	private CategorySummary summary;

	private int previousRecalculation = 0;

	public int[] Prototype => summary.Prototype;

	public CategorySummary Summary => summary;

	public int Id => categoryId;

	public Category(ICategoryDeterminer categoryDeterminer, int id)
	{
		this.categoryDeterminer = categoryDeterminer;

		observations = new List<Observation>();
		summary = new CategorySummary();
		categoryId = id;
	}

	public void AddObservation(Observation observation)
	{
		observations.Add(observation);

		DetermineAndRecalculateCategorySummary();
	}

	private void DetermineAndRecalculateCategorySummary()
	{
		if (observations.Count >= 5 && Prototype.Length == 0)
		{
			RecalculateCategorySummary();
			return;
		}

		if (observations.Count - previousRecalculation >= 5)
		{
			RecalculateCategorySummary();
			return;
		}

		//only for testing
		if (observations.Count == 13)
		{
			RecalculateCategorySummary();
			return;
		}
	}

	private void RecalculateCategorySummary()
	{
		summary = categoryDeterminer.DetermineCategory(observations.ToArray());
		previousRecalculation = observations.Count;

		//to remove
		DisplayCategorySummary();
	}

	public void DisplayCategorySummary()
	{
		Console.WriteLine(summary.ToString());
	}
}
