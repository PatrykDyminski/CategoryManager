using CategoryManager.Model;

namespace CategoryManager.Manager
{
	internal interface ICategoryManager
	{
		void AddObservation(Observation observation);
		void AddObservationsBatch(IEnumerable<Observation> observations);
	}
}