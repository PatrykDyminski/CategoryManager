using CategoryManager.CategoryDeterminer;
using CategoryManager.Distance;
using CategoryManager.Model;
using CSharpFunctionalExtensions;

namespace CategoryManager.Category;

public class Category : ICategory
{
	private readonly ICategoryDeterminer categoryDeterminer;
	private readonly IDistance distance;

	private readonly int categoryId;
	private readonly List<Observation> observations;
	private Maybe<CategorySummary> summary;

	private readonly List<Observation> coreObservations;
	private readonly List<Observation> boundaryObservations;

	private int previousRecalculation = 0;

	public Maybe<int[]> Prototype => summary.Map(x => x.Prototype);

	public Maybe<CategorySummary> Summary => summary;

	public int Id => categoryId;

	public Category(ICategoryDeterminer categoryDeterminer, IDistance distance, int id)
	{
		this.categoryDeterminer = categoryDeterminer;
		this.distance = distance;

		observations = new List<Observation>();
		coreObservations = new List<Observation>();
		boundaryObservations = new List<Observation>();

		summary = Maybe.None;
		categoryId = id;
	}

	public bool AddObservation(Observation observation)
	{
		var hasRecalculated = DetermineAndRecalculateCategorySummary();

		//jeśli nie zrekalkulowano. Bo w przypadku rekalkulacji obserwacja przypisana w metodzie wyżej
		if (!hasRecalculated)
		{
			AddObservationToProperGroup(observation);
		}

		return hasRecalculated;
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

	private void AddObservationToProperGroup(Observation observation)
	{
		//TODO currently observations are duplicated. Handle it.
		observations.Add(observation);

		if (summary.HasValue)
		{
			var distanceFromProto = distance.CalculateDistance(summary.Value.Prototype, observation.ObservedObject);
			if (distanceFromProto < summary.Value.Tplus)
			{
				coreObservations.Add(observation);
			}
			else if (distanceFromProto < summary.Value.Tminus)
			{
				//TODO Podzielić Boundary
				boundaryObservations.Add(observation);
			}
			//TODO co z outer?
		}
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
				coreObservations.AddRange(x.CoreObservations);
				boundaryObservations.AddRange(x.BoundaryObservations);

				DisplayCategorySummary();
			});

		return result.IsSuccess;
	}

	public void DisplayCategorySummary()
	{
		Console.WriteLine(summary.ToString());
	}
}
