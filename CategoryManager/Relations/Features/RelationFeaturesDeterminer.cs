using CategoryManager.Distance;
using CategoryManager.Model;

namespace CategoryManager.Relations.Features;

public class RelationFeaturesDeterminer
{
	private readonly IDistance macrostructure;

	public RelationFeaturesDeterminer(IDistance macrostructure)
	{
		this.macrostructure = macrostructure;
	}

	public (bool result, double common) BoundaryCross(CategorySummary c1, CategorySummary c2, double distance = double.NaN)
	{
		var dst = GetDistance(c1, c2, distance);

		//B1 + B2 - Distance > Tresh
		var cross = c1.Tminus + c2.Tminus - dst;

		return cross > 0
			? (true, cross)
			: (false, -1);
	}

	public (bool result, double common) CoreCross(CategorySummary c1, CategorySummary c2, double distance = double.NaN)
	{
		var dst = GetDistance(c1, c2, distance);

		//C1 + C2 - Distance > Tresh
		var cross = c1.Tplus + c2.Tplus - dst;

		return cross > 0
			? (true, cross)
			: (false, -1);
	}

	public (bool result, double common) CoreInsideCore(CategorySummary c1, CategorySummary c2, double distance = double.NaN)
	{
		var dst = GetDistance(c1, c2, distance);

		(CategorySummary smaller, CategorySummary bigger) = c1.Tplus < c2.Tplus
			? (c1, c2)
			: (c2, c1);

		//CD - CM - D > Tresh
		var inside = bigger.Tplus - smaller.Tplus - dst;

		return inside > 0
			? (true, inside)
			: (false, -1);
	}

	public (bool result, double common) BoundaryInsideBoundary(CategorySummary c1, CategorySummary c2, double distance = double.NaN)
	{
		var dst = GetDistance(c1, c2, distance);

		(CategorySummary smaller, CategorySummary bigger) = c1.Tminus < c2.Tminus
			? (c1, c2)
			: (c2, c1);

		//BD - BM - D > Tresh
		var inside = bigger.Tminus - smaller.Tminus - dst;

		return inside > 0
			? (true, inside)
			: (false, -1);
	}


	private double GetDistance(CategorySummary c1, CategorySummary c2, double distance)
	{
		return double.IsNaN(distance)
			? macrostructure.CalculateDistance(c1.Prototype, c2.Prototype)
			: distance;
	}
}
