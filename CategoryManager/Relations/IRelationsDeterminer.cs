using CategoryManager.Model;

namespace CategoryManager.Relations;

public interface IRelationsDeterminer
{
	bool DetermineSpecification(CategorySummary c1, CategorySummary c2);

	bool ValidateRelation(Relation relation);

	List<Relation> GetRelationsForCategories(int cat1id, int cat2id, CategorySummary c1, CategorySummary c2);
}
