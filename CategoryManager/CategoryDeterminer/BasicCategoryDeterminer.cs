using CategoryManager.Candidates;
using CategoryManager.Macrostructure;
using CategoryManager.Model;
using CategoryManager.Utils;
using CSharpFunctionalExtensions;
using System.Collections.Immutable;

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

  public Result<CategoryDeterminationResultDTO> DetermineCategory(Observation[] observations)
  {
    int[] prototype = Array.Empty<int>();

    var positiveObservations = observations.Where(x => x.IsRelated);
    var negativeObservations = observations.Where(x => !x.IsRelated);

    var positiveObservationsSet = positiveObservations.DistinctBy(x => x.ObservedObject, new ObservedObjectComparer());
    var negativeObservationsSet = negativeObservations.DistinctBy(x => x.ObservedObject, new ObservedObjectComparer());

    var candidates = candidatesExtractor.ExtractCandidates(
      positiveObservations
        .Select(x => x.ObservedObject)
        .ToArray());

    //only object that has positive ovservations (no negative) can become prototype
    var filteredCandidates = candidates
      .Where(cand => !negativeObservationsSet
        .Select(x => x.ObservedObject)
        .Contains(cand, new ObservedObjectComparer())
       );

    foreach (var prototypeCandidate in filteredCandidates)
    {
      var positiveDistances = positiveObservationsSet
        .Select(x => new
        {
          Observation = x,
          Distance = macrostructure.CalculateDistance(prototypeCandidate, x.ObservedObject)
        });

      var negativeDistances = negativeObservationsSet
        .Select(x => new
        {
          Observation = x,
          Distance = macrostructure.CalculateDistance(prototypeCandidate, x.ObservedObject)
        });

      //not enough observations
      if (!positiveDistances.Any() || !negativeDistances.Any())
      {
        return Result.Failure<CategoryDeterminationResultDTO>("Could not determine category - not enough observations");
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
        return Result.Failure<CategoryDeterminationResultDTO>("Could not determine category");
      }

      var core = tauPlus.GetValueOrDefault(
        tp => positiveDistances
          .Where(x => x.Distance <= tp)
          .Select(y => y.Observation),
        Array.Empty<Observation>());

      var outer = tauMinus.GetValueOrDefault(
        tm => negativeDistances
          .Where(x => x.Distance >= tm)
          .Select(y => y.Observation),
        Array.Empty<Observation>());

      //boundary = positive and negative objects not present in the core and outer
      var boundary = positiveObservationsSet
        .Concat(negativeObservationsSet)
        .Except(core
          .Concat(outer), new ObservationComparer());

      //more objects in the core than positive objects in the boundary
      if (core.Count() >= positiveObservationsSet
        .Intersect(boundary, new ObservationComparer())
        .Count())
      {
        var summary = new CategorySummary
        {
          Prototype = prototypeCandidate,
          Tplus = tauPlus.Value,
          Tminus = tauMinus.Value,
        };

        var ret = new CategoryDeterminationResultDTO
        {
          Summary = summary,
          CoreObservationSet = core.ToImmutableHashSet(),
          BoundaryObservationSet = boundary.ToImmutableHashSet()
        };

        return ret;
      }
    }

    return Result.Failure<CategoryDeterminationResultDTO>("Could not determine category");
  }
}
