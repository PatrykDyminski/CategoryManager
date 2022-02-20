using CategoryManager.Candidates;
using CategoryManager.Distance;
using CategoryManager.Model;
using CategoryManager.Utils;

namespace CategoryManager.Categories;

public class BasicCategoryDeterminer : ICategoryDeterminer
{
	public Category DetermineCategory(Observation[] observations, IDistance macrostructure, ICandidatesExtractor candidatesExtractor)
	{
		int[] prototype = Array.Empty<int>();

		var positiveObservations = observations.Where(x => x.IsRelated);
		var negativeObservations = observations.Where(x => !x.IsRelated);

		var positiveObservationsSet = positiveObservations.DistinctBy(x => x.ObservedObject, new IntArrayComparer());
		var negativeObservationsSet = negativeObservations.DistinctBy(x => x.ObservedObject, new IntArrayComparer());

		var candidates = candidatesExtractor.ExtractCandidates(
			positiveObservations.Select(x => x.ObservedObject).ToArray(), 
			macrostructure);

		foreach (var prototypeCandidate in candidates)
		{
			var positiveDistances = positiveObservationsSet
				.Select(x => new { x.ObservedObject ,Distance = macrostructure.CalculateDistance(prototypeCandidate, x.ObservedObject)});

			var negativeDistances = negativeObservationsSet
				.Select(x => new { x.ObservedObject, Distance = macrostructure.CalculateDistance(prototypeCandidate, x.ObservedObject)});

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

			var boundary = positiveObservationsSet
				.Select(x => x.ObservedObject)
				.Concat(negativeObservationsSet
					.Select(x => x.ObservedObject))
				.Except(core
					.Concat(outer), new IntArrayComparer());

			if(core.Count() >= positiveObservationsSet.Select(x => x.ObservedObject).Intersect(boundary, new IntArrayComparer()).Count())
			{
				return new Category
				{
					Prototype = prototypeCandidate,
					Tplus = tauPlus,
					Tminus = tauMinus,
				};
			}
		}

		return new Category
		{
			Prototype = Array.Empty<int>(),
			Tplus = 0,
			Tminus = 0,
		};
	}
}
