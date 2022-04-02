using CategoryManager.Category;
using CategoryManager.Model;

namespace CategoryManager.Repository.Interfaces;

public interface IRelationsRepository
{
	void UpdateRelations(ICategory category);

	IReadOnlyCollection<Relation> GetAllRelations();

	IReadOnlyCollection<Relation> GetRelationsForCategory(int categoryId);
}
