using CategoryManager.Model;
using CategoryManager.Repository.Interfaces;

namespace CategoryManager.Manager;

internal class CategoryManager
{
	private readonly ICategoryRepository categoryRepository;
	private readonly IRelationsRepository relationsRepository;

	public CategoryManager(ICategoryRepository categoryRepository, IRelationsRepository relationsRepository)
	{
		this.categoryRepository = categoryRepository;
		this.relationsRepository = relationsRepository;
	}

	public void AddObservation(Observation observation)
	{
		var recalculated = categoryRepository.AddObservation(observation);

		if (recalculated)
		{
			relationsRepository.UpdateRelations(observation.CategoryId);
		}
	}
}
