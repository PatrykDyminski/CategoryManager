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

	public bool AddObservation(Observation observation)
	{
		observations.Add(observation);

		return DetermineAndRecalculateCategorySummary();
	}

	private bool DetermineAndRecalculateCategorySummary()
	{
		//first callculation after receiving 5 obervations
		if (observations.Count >= 5 && Prototype.HasNoValue)
		{
			return RecalculateCategorySummary();
		}

		//next recalculation after.....
		if (observations.Count - previousRecalculation >= 5)
		{
			return RecalculateCategorySummary();
		}

		//only for testing
		if (observations.Count == 13)
		{
			return RecalculateCategorySummary();
		}

		return false;
	}

	private bool RecalculateCategorySummary()
	{
		var result = categoryDeterminer
			.DetermineCategory(observations.ToArray())
			.Tap(x =>
			{
				summary = x;
				previousRecalculation = observations.Count;

				DisplayCategorySummary();
			});

		return result.IsSuccess;
	}

	public void DisplayCategorySummary()
	{
		Console.WriteLine(summary.ToString());
	}
}
