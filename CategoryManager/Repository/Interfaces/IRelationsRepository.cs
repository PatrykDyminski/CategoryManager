using CategoryManager.Category;
using CategoryManager.Relations.Types;

namespace CategoryManager.Repository.Interfaces;

public interface IRelationsRepository
{
	void UpdateRelations(ICategory category);

	IReadOnlyCollection<IRelation> GetAllRelations();

	IReadOnlyCollection<IRelation> GetRelationsForCategory(int categoryId);
}
