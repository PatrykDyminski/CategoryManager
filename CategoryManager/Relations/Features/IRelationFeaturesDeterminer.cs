using CategoryManager.Category;
using CategoryManager.Model;
using CSharpFunctionalExtensions;

namespace CategoryManager.Relations.Features
{
	public interface IRelationFeaturesDeterminer
	{
		Maybe<double> BoundaryCross(CategorySummary c1, CategorySummary c2, Maybe<double> distance);
		Maybe<(double common, int bigger)> BoundaryInsideBoundary(CategorySummary c1, CategorySummary c2, Maybe<double> distance);
		Maybe<double> CoreCross(CategorySummary c1, CategorySummary c2, Maybe<double> distance);
		Maybe<(double common, int bigger)> CoreInsideCore(CategorySummary c1, CategorySummary c2, Maybe<double> distance);

		double IntersectionRatio(ISet<Observation> observations1, ISet<Observation> observations2);

		bool PrototypesInsideCores(ICategory cat1, ICategory cat2);
	}
}