using CategoryManager.Model;

namespace CategoryManager.Repository.Interfaces;

public interface IRelationsRepository
{
	void UpdateRelations(int categoryId, CategorySummary categorySummary);
}
