using CategoryManager.Model;

namespace CategoryManager.Relations.Features
{
	public interface IRelationFeaturesDeterminer
	{
		(bool result, double common) BoundaryCross(CategorySummary c1, CategorySummary c2, double distance = double.NaN);
		(bool result, double common) BoundaryInsideBoundary(CategorySummary c1, CategorySummary c2, double distance = double.NaN);
		(bool result, double common) CoreCross(CategorySummary c1, CategorySummary c2, double distance = double.NaN);
		(bool result, double common) CoreInsideCore(CategorySummary c1, CategorySummary c2, double distance = double.NaN);
	}
}