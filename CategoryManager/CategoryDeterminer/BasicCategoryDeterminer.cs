using CategoryManager.Candidates;
using CategoryManager.Distance;
using CategoryManager.Model;
using CategoryManager.Utils;

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

	public CategorySummary DetermineCategory(Observation[] observations)
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
				return ReturnEmptyCategory();
			}

			var minimalNegativeDistance = negativeDistances.MinBy(x => x.Distance).Distance;
			var maximalPositiveDistance = positiveDistances.MaxBy(x => x.Distance).Distance;

			var fPlus = positiveDistances.Where(x => x.Distance < minimalNegativeDistance);
			var tauPlus = fPlus.Any()
				? fPlus.MaxBy(x => x.Distance).Distance
				: -1;

			var fMinus = negativeDistances.Where(x => x.Distance > maximalPositiveDistance);
			var tauMinus = fMinus.Any()
				? fMinus.MinBy(x => x.Distance).Distance
				: -1;

			var core = tauPlus > -1
				? positiveDistances.Where(x => x.Distance <= tauPlus).Select(y => y.ObservedObject)
				: Array.Empty<int[]>();

			var outer = tauMinus > -1
				? negativeDistances.Where(x => x.Distance >= tauMinus).Select(y => y.ObservedObject)
				: Array.Empty<int[]>();

			//boundary is positive and negative objects not present in the core and outer
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
					Tplus = tauPlus,
					Tminus = tauMinus,
				};
			}
		}

		return ReturnEmptyCategory();
	}

	private CategorySummary ReturnEmptyCategory()
	{
		return new CategorySummary
		{
			Prototype = Array.Empty<int>(),
			Tplus = 0,
			Tminus = 0,
		};
	}
}
