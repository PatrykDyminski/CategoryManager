using CategoryManager.Model;
using CSharpFunctionalExtensions;

namespace CategoryManager.Manager
{
	internal interface ICategoryManager
	{
		void AddObservation(Observation observation);
		void AddObservationsBatch(IEnumerable<Observation> observations);
		Result<string> GetCategorySummary(int categotyId);
	}
}