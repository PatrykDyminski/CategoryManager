using CategoryManager.Model;

namespace CategoryManager.CategoryDeterminer;

public class CategoryDeterminationResultDTO
{
	public CategorySummary Summary { get; set; }
	public ISet<Observation> CoreObservationSet { get; set; }
	public ISet<Observation> BoundaryObservationSet { get; set; }
}
