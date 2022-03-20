using CategoryManager.Model;

namespace CategoryManager.Utils;

internal class ObservationsGenerator
{

	//TODO Tune this method XDD
	public static List<Observation> GenerateObservations(int catId, int[] prototype, double core, double boundary, int count)
	{
		List<Observation> observations = new List<Observation>();
		Random rnd = new();

		//prototype
		for (int i = 0; i < count * 1 / 20; i++)
		{
			observations.Add(new Observation
			{
				CategoryId = catId,
				IsRelated = true,
				ObservedObject = (int[])prototype.Clone()
			});
		}

		//core
		for (int i = 0; i < count * 9 / 20; i++)
		{
			observations.Add(new Observation
			{
				CategoryId = catId,
				IsRelated = true,
				ObservedObject = MutateBetween((int[])prototype.Clone(), (int)core+1, 1)
			});
		}

		//boundary
		for (int i = 0; i < count * 4 / 10; i++)
		{
			observations.Add(new Observation
			{
				CategoryId = catId,
				IsRelated = rnd.Next(2) == 0,
				ObservedObject = MutateBetween((int[])prototype.Clone(), (int)boundary, (int)core)
			});
		}

		//outer
		for (int i = 0; i < count * 1 / 10; i++)
		{
			observations.Add(new Observation
			{
				CategoryId = catId,
				IsRelated = false,
				ObservedObject = MutateBetween((int[])prototype.Clone(), prototype.Length, (int)boundary)
			});
		}

		//shuffle before returning
		return observations
			.OrderBy(t => rnd.Next())
			.ToList();
	}

	private static int[] MutateLess(int[] prototype, int lessThanCount)
	{
		Random rnd = new();

		var count = rnd.Next(0, lessThanCount + 1);

		var intArray = Enumerable
			.Range(0, prototype.Length)
			.OrderBy(t => rnd.Next())
			.Take(count)
			.ToArray();

		for (int i = 0; i < count; i++)
		{
			prototype[intArray[i]] = prototype[intArray[i]] == 0
				? 1
				: 0;
		}

		return prototype;
	}

	private static int[] MutateBetween(int[] prototype, int lessThanCount, int moreOrEqualThanCount)
	{
		Random rnd = new();

		var count = rnd.Next(moreOrEqualThanCount, lessThanCount);

		var intArray = Enumerable
			.Range(0, prototype.Length)
			.OrderBy(t => rnd.Next())
			.Take(count)
			.ToArray();

		for (int i = 0; i < count; i++)
		{
			prototype[intArray[i]] = prototype[intArray[i]] == 0
				? 1
				: 0;
		}

		return prototype;
	}
}
