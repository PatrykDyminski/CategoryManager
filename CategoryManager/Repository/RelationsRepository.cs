using CategoryManager.Model;
using CategoryManager.Repository.Interfaces;

namespace CategoryManager.Repository;

internal class RelationsRepository : IRelationsRepository
{
	private List<Relation> Relationships;

	public RelationsRepository()
	{
		Relationships = new List<Relation>();
	}

	public void UpdateRelations(int categoryId)
	{
		Console.WriteLine("Updating Relations..........");
	}
}
