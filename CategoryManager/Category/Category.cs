using CategoryManager.CategoryDeterminer;
using CategoryManager.Distance;
using CategoryManager.Model;
using CategoryManager.Utils;
using CSharpFunctionalExtensions;
using System.Collections.Immutable;

namespace CategoryManager.Category;

public class Category : ICategory
{
	private readonly ICategoryDeterminer categoryDeterminer;
	private readonly IDistance distance;

	private readonly int categoryId;
	private readonly List<Observation> observations;
	private Maybe<CategorySummary> summary;

	private readonly ISet<Observation> coreObservationSet;
	private readonly ISet<Observation> boundaryObservationSet;

	private int previousRecalculation = 0;

	public Maybe<int[]> Prototype => summary.Map(x => x.Prototype);

	public Maybe<CategorySummary> Summary => summary;

	public int Id => categoryId;

	public Category(ICategoryDeterminer categoryDeterminer, IDistance distance, int id)
	{
		this.categoryDeterminer = categoryDeterminer;
		this.distance = distance;

		observations = new List<Observation>();

		coreObservationSet = new HashSet<Observation>(new ObservationComparerWithRelation());
		boundaryObservationSet = new HashSet<Observation>(new ObservationComparerWithRelation());

		summary = Maybe.None;
		categoryId = id;
	}

	public bool AddObservation(Observation observation)
	{
		observations.Add(observation);

		var hasRecalculated = DetermineAndRecalculateCategorySummary();

		return hasRecalculated;
	}

  public Maybe<ImmutableHashSet<Observation>> GetCoreObservations() => 
		coreObservationSet.Any()
      ? Maybe.From(coreObservationSet.ToImmutableHashSet())
      : Maybe.None;

  public Maybe<ImmutableHashSet<Observation>> GetPositiveBoundaryObservations() =>
		boundaryObservationSet.Any()
      ? Maybe.From(coreObservationSet.ToImmutableHashSet())
      : Maybe.None;

  public void DisplayCategorySummary()
	{
		Console.WriteLine(summary.ToString());
	}

	private bool DetermineAndRecalculateCategorySummary()
	{
		//first callculation after receiving 5 obervations
		if (observations.Count >= 5 && Prototype.HasNoValue)
		{
			return RecalculateCategorySummary();
		}

		//next recalculation after.....
		if (observations.Count - previousRecalculation >= 0.1 * previousRecalculation)
		{
			return RecalculateCategorySummary();
		}

		return false;
	}

	private bool RecalculateCategorySummary()
	{
		Console.WriteLine("Recalc + " + observations.Count);

		//save even if no succesfull recalculation
		previousRecalculation = observations.Count;

		var result = categoryDeterminer
			.DetermineCategory(observations.ToArray())
			.Tap(x =>
			{
				summary = x.Summary;
				coreObservationSet.UnionWith(x.CoreObservationSet);
				boundaryObservationSet.UnionWith(x.BoundaryObservationSet);

				DisplayCategorySummary();
			});

		return result.IsSuccess;
	}
}
