using CategoryManager.Distance;
using CategoryManager.Model;
using CategoryManager.Utils;
using CSharpFunctionalExtensions;

namespace CategoryManager.Relations.Features;

public class RelationFeaturesDeterminer : IRelationFeaturesDeterminer
{
  private readonly IDistance macrostructure;

  public RelationFeaturesDeterminer(IDistance macrostructure)
  {
    this.macrostructure = macrostructure;
  }

  public Maybe<double> BoundaryCross(CategorySummary c1, CategorySummary c2, Maybe<double> distance)
  {
    var dst = GetDistance(c1, c2, distance);

    //B1 + B2 - Distance > Tresh
    var cross = c1.Tminus + c2.Tminus - dst;

    return cross > 0
      ? Maybe.From(cross)
      : Maybe.None;
  }

  public Maybe<double> CoreCross(CategorySummary c1, CategorySummary c2, Maybe<double> distance)
  {
    var dst = GetDistance(c1, c2, distance);

    //C1 + C2 - Distance > Tresh
    var cross = c1.Tplus + c2.Tplus - dst;

    return cross > 0
      ? Maybe.From(cross)
      : Maybe.None;
  }

  public Maybe<(double common, int bigger)> CoreInsideCore(CategorySummary c1, CategorySummary c2, Maybe<double> distance)
  {
    var dst = GetDistance(c1, c2, distance);

    (CategorySummary smaller, CategorySummary bigger, int biggerIndex) = c1.Tplus < c2.Tplus
      ? (c1, c2, 2)
      : (c2, c1, 1);

    //CD - CM - D > Tresh
    var inside = bigger.Tplus - smaller.Tplus - dst;

    return inside > 0
      ? Maybe.From((inside, biggerIndex))
      : Maybe.None;
  }

  public Maybe<(double common, int bigger)> BoundaryInsideBoundary(CategorySummary c1, CategorySummary c2, Maybe<double> distance)
  {
    var dst = GetDistance(c1, c2, distance);

    (CategorySummary smaller, CategorySummary bigger, int biggerIndex) = c1.Tminus < c2.Tminus
      ? (c1, c2, 2)
      : (c2, c1, 1);

    //BD - BM - D > Tresh
    var inside = bigger.Tminus - smaller.Tminus - dst;

    return inside > 0
      ? Maybe.From((inside, biggerIndex))
      : Maybe.None;
  }

  public Maybe<double> IntersectionRatio(ISet<Observation> observations1, ISet<Observation> observations2)
  {
    var intersection = observations1.Intersect(observations2, new ObservationComparer());
    var sum = observations1.Union(observations2, new ObservationComparer());

    if (intersection.Any() && sum.Any())
    {
      double ratio = (double)intersection.Count() / (double)sum.Count();
      
      return Maybe<double>.From(ratio);
    }
    else
    {
      return Maybe.None;
    }
  }

  private double GetDistance(CategorySummary c1, CategorySummary c2, Maybe<double> distance)
  {
    return distance.HasValue
      ? distance.Value
      : macrostructure.CalculateDistance(c1.Prototype, c2.Prototype);
  }
}
