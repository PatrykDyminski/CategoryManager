using CategoryManager.CategoryDeterminer;
using CategoryManager.Model;
using CSharpFunctionalExtensions;

namespace CategoryManager.Category;

public class Category : ICategory
{
	private readonly ICategoryDeterminer categoryDeterminer;

	private readonly int categoryId;
	private readonly List<Observation> observations;
	private Maybe<CategorySummary> summary;

	private int previousRecalculation = 0;

	public Maybe<int[]> Prototype => summary.Map(x => x.Prototype);

	public Maybe<CategorySummary> Summary => summary;

	public int Id => categoryId;

	public Category(ICategoryDeterminer categoryDeterminer, int id)
	{
		this.categoryDeterminer = categoryDeterminer;

		observations = new List<Observation>();
		summary = Maybe.None;
		categoryId = id;
	}

	public void AddObservation(Observation observation)
	{
		observations.Add(observation);

		DetermineAndRecalculateCategorySummary();
	}

	private void DetermineAndRecalculateCategorySummary()
	{
		//first callculation after receiving 5 obervations
		if (observations.Count >= 5 && Prototype.HasNoValue)
		{
			RecalculateCategorySummary();
			return;
		}

		//next recalculation after.....
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
		categoryDeterminer
			.DetermineCategory(observations.ToArray())
			.Tap(x =>
			{
				summary = x;
				previousRecalculation = observations.Count;
			});

		//TODO to remove
		DisplayCategorySummary();
	}

	public void DisplayCategorySummary()
	{
		Console.WriteLine(summary.ToString());
	}
}
