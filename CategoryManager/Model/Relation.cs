using CategoryManager.Relations;

namespace CategoryManager.Model;

public record Relation
{
	public int Cat1Id { get; set; }
	public int Cat2Id { get; set; }
	//TODO Consider deleting summaries from here
	public CategorySummary CategorySummary1 { get; set; }
	public CategorySummary CategorySummary2 { get; set; }
	public RelationType relationType { get; set; }
}
