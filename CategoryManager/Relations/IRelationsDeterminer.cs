using CategoryManager.Model;

namespace CategoryManager.Relations;

public interface IRelationsDeterminer
{
	bool DetermineSpecification(CategorySummary c1, CategorySummary c2);
}
