using CategoryManager.CategoryDeterminer;
using CategoryManager.Macrostructure;
using CategoryManager.Model;
using CategoryManager.Utils;
using CSharpFunctionalExtensions;
using System.Collections.Immutable;

namespace CategoryManager.Category;

public class Category : ICategory
{
	private readonly ICategoryDeterminer categoryDeterminer;
	private readonly IDistance macrostructure;

	private readonly int categoryId;
	private readonly List<Observation> observations;
	private Maybe<CategorySummary> summary;

	private ISet<Observation> coreObservationSet;
	private ISet<Observation> boundaryObservationSet;

	private int previousRecalculation = 0;

	public Maybe<int[]> Prototype => summary.Map(x => x.Prototype);

	public Maybe<CategorySummary> Summary => summary;

	public int Id => categoryId;

	public Category(ICategoryDeterminer categoryDeterminer, IDistance macrostructure, int id)
	{
		this.categoryDeterminer = categoryDeterminer;
		this.macrostructure = macrostructure;

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

  public string GetCategorySummary()
	{
		var core = coreObservationSet.Select(x => "	" + x.ObservedObject.AsString() + $" - Dst: {macrostructure.CalculateDistance(x.ObservedObject, Prototype.Value)}").ToList();
		var coreSummary = "\nCore:\n" + string.Join("\n", core);

		var boundary = boundaryObservationSet.Select(x => "	" + x.ObservedObject.AsString() + $" - Dst: {macrostructure.CalculateDistance(x.ObservedObject, Prototype.Value)} - rel: " + x.IsRelated).ToList();
		var boundarySummary = "\nBoundary:\n" + string.Join("\n", boundary);

		return $"CategoryId: {categoryId}, " + summary.ToString() + coreSummary + boundarySummary;
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
		//TODO Remove 
		Console.WriteLine("Recalc + " + observations.Count);

		//save even if no succesfull recalculation
		previousRecalculation = observations.Count;

		var result = categoryDeterminer
			.DetermineCategory(observations.ToArray())
			.Tap(x =>
			{
				summary = x.Summary;
				coreObservationSet = x.CoreObservationSet;
				boundaryObservationSet = x.BoundaryObservationSet;

				Console.WriteLine("Ding new summary");
				//Console.WriteLine(GetCategorySummary());
			});

		return result.IsSuccess;
	}
}
