using CategoryManager.Model;
using CategoryManager.Relations.Features;

namespace CategoryManager.Relations;

public class RelationsDeterminer : IRelationsDeterminer
{
	private readonly IRelationFeaturesDeterminer rfd;

	public RelationsDeterminer(IRelationFeaturesDeterminer rfd)
	{
		this.rfd = rfd;
	}

	//only example
	public bool DetermineSpecification(CategorySummary c1, CategorySummary c2)
	{
		var op1 = rfd.CoreInsideCore(c1, c2);
		var op2 = rfd.BoundaryInsideBoundary(c1, c2);

		if(op1.result && op2.result)
		{
			return true;	
		}

		return false;
	}
}
