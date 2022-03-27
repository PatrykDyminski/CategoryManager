using CategoryManager.Model;

namespace CategoryManager.CategoryDeterminer;

public class CategoryDeterminationResultDTO
{
	public CategorySummary Summary { get; set; }
	public List<Observation> CoreObservations { get; set; }
	public List<Observation> BoundaryObservations { get; set; }
}
