using CategoryManager.Candidates;
using CategoryManager.Distance;
using CategoryManager.Model;
using CategoryManager.Utils;
using CSharpFunctionalExtensions;

namespace CategoryManager.CategoryDeterminer;

public class BasicCategoryDeterminer : ICategoryDeterminer
{
	private readonly IDistance macrostructure;
	private readonly ICandidatesExtractor candidatesExtractor;

	public BasicCategoryDeterminer(IDistance macrostructure, ICandidatesExtractor candidatesExtractor)
	{
		this.macrostructure = macrostructure;
		this.candidatesExtractor = candidatesExtractor;
	}

	public Result<CategorySummary> DetermineCategory(Observation[] observations)
	{
		int[] prototype = Array.Empty<int>();

		var positiveObservations = observations.Where(x => x.IsRelated);
		var negativeObservations = observations.Where(x => !x.IsRelated);

		var positiveObservationsSet = positiveObservations.DistinctBy(x => x.ObservedObject, new IntArrayComparer());
		var negativeObservationsSet = negativeObservations.DistinctBy(x => x.ObservedObject, new IntArrayComparer());

		var candidates = candidatesExtractor.ExtractCandidates(
			positiveObservations
				.Select(x => x.ObservedObject)
				.ToArray());

		foreach (var prototypeCandidate in candidates)
		{
			var positiveDistances = positiveObservationsSet
				.Select(x => new { x.ObservedObject, Distance = macrostructure.CalculateDistance(prototypeCandidate, x.ObservedObject) });

			var negativeDistances = negativeObservationsSet
				.Select(x => new { x.ObservedObject, Distance = macrostructure.CalculateDistance(prototypeCandidate, x.ObservedObject) });

			//not enough observations
			if(!positiveDistances.Any() || !negativeDistances.Any())
			{
				return Result.Failure<CategorySummary>("Could not determine category - not enough observations");
			}

			var minimalNegativeDistance = negativeDistances.MinBy(x => x.Distance)!.Distance;
			var maximalPositiveDistance = positiveDistances.MaxBy(x => x.Distance)!.Distance;

			var fPlus = positiveDistances.Where(x => x.Distance < minimalNegativeDistance);
			var tauPlus = fPlus.Any()
				? Maybe.From(fPlus.MaxBy(x => x.Distance)!.Distance)
				: Maybe.None;

			var fMinus = negativeDistances.Where(x => x.Distance > maximalPositiveDistance);
			var tauMinus = fMinus.Any()
				? Maybe.From(fMinus.MinBy(x => x.Distance)!.Distance)
				: Maybe.None;

			if (tauPlus.HasNoValue || tauMinus.HasNoValue)
			{
				return Result.Failure<CategorySummary>("Could not determine category");
			}

			var core = tauPlus.GetValueOrDefault(
				tp => positiveDistances
					.Where(x => x.Distance <= tp)
					.Select(y => y.ObservedObject), 
				Array.Empty<int[]>());

			var outer = tauMinus.GetValueOrDefault(
				tm => negativeDistances
					.Where(x => x.Distance >= tm)
					.Select(y => y.ObservedObject),
				Array.Empty<int[]>());

			//boundary = positive and negative objects not present in the core and outer
			var boundary = positiveObservationsSet
				.Select(x => x.ObservedObject)
				.Concat(negativeObservationsSet
					.Select(x => x.ObservedObject))
				.Except(core
					.Concat(outer), new IntArrayComparer());

			//more objects in the core than positive objects in the boundary
			if (core.Count() >= positiveObservationsSet
				.Select(x => x.ObservedObject)
				.Intersect(boundary, new IntArrayComparer())
				.Count())
			{
				return new CategorySummary
				{
					Prototype = prototypeCandidate,
					Tplus = tauPlus.Value,
					Tminus = tauMinus.Value,
				};
			}
		}

		return Result.Failure<CategorySummary>("Could not determine category");
	}
}
