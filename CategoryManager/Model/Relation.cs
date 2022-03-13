using CategoryManager.Relations;

namespace CategoryManager.Model;

public class Relation
{
	public int Cetegory1Id { get; }
	public int Cetegory2Id { get; }
	public RelationType relationType { get; }
}
