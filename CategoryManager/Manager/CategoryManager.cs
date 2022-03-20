using CategoryManager.Model;
using CategoryManager.Repository.Interfaces;
using CSharpFunctionalExtensions;

namespace CategoryManager.Manager;

internal class CategoryManager : ICategoryManager
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
			categoryRepository
				.GetCategorySummaryById(observation.CategoryId)
				.Tap(x => relationsRepository.UpdateRelations(observation.CategoryId, x));
		}
	}
}
