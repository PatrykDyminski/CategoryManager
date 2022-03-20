using CategoryManager.Model;
using CategoryManager.Relations.Features;
using CSharpFunctionalExtensions;

namespace CategoryManager.Relations;

public class RelationsDeterminer : IRelationsDeterminer
{
	private readonly IRelationFeaturesDeterminer rfd;

	private readonly Dictionary<RelationType, Func<CategorySummary, CategorySummary, bool>> determiners;

	public RelationsDeterminer(IRelationFeaturesDeterminer rfd)
	{
		this.rfd = rfd;

		determiners = new Dictionary<RelationType, Func<CategorySummary, CategorySummary, bool>>
		{
			{ RelationType.Specification, DetermineSpecification }
		};
	}

	//only example
	//TODO To refactor to distinguish left and right
	public bool DetermineSpecification(CategorySummary c1, CategorySummary c2)
	{
		var op1 = rfd.CoreInsideCore(c1, c2, Maybe.None);
		var op2 = rfd.BoundaryInsideBoundary(c1, c2, Maybe.None);

		if(op1.HasValue && op2.HasValue && op1.Value.bigger == op2.Value.bigger)
		{
			return true;	
		}

		return false;
	}

	public bool ValidateRelation(Relation relation)
	{
		return determiners[relation.relationType](relation.CategorySummary1, relation.CategorySummary2);
	}

	public List<Relation> GetRelationsForCategories(int cat1id, int cat2id, CategorySummary c1, CategorySummary c2)
	{
		//TODO Implemet this when more relations added
		var spec = DetermineSpecification(c1, c2);

		if (spec)
		{
			return new List<Relation>
			{
				new Relation { Cat1Id = cat1id, Cat2Id = cat2id, CategorySummary1 = c1, CategorySummary2 = c2, relationType = RelationType.Specification }
			};
		}

		return new List<Relation>();
	}
}
