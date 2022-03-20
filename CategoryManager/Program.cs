using CategoryManager.Candidates;
using CategoryManager.CategoryDeterminer;
using CategoryManager.Distance;
using CategoryManager.Manager;
using CategoryManager.Model;
using CategoryManager.Relations;
using CategoryManager.Relations.Features;
using CategoryManager.Repository;
using CategoryManager.Repository.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

Observation[] observations = new Observation[]
	{
		//related
		//1
		new Observation{ CategoryId = 1, IsRelated = true, ObservedObject = new int[] {0,0,0,0 } },

		//3
		new Observation{ CategoryId = 1, IsRelated = true, ObservedObject = new int[] {0,0,1,0 } },

		//4
		new Observation{ CategoryId = 1, IsRelated = true, ObservedObject = new int[] {0,0,1,1 } },
		new Observation{ CategoryId = 1, IsRelated = true, ObservedObject = new int[] {0,0,1,1 } },

		//6
		new Observation{ CategoryId = 1, IsRelated = true, ObservedObject = new int[] {0,1,0,1 } },
		new Observation{ CategoryId = 1, IsRelated = true, ObservedObject = new int[] {0,1,0,1 } },

		//not related
		//6
		new Observation{ CategoryId = 1, IsRelated = false, ObservedObject = new int[] {0,1,0,1 } },

		//10
		new Observation{ CategoryId = 1, IsRelated = false, ObservedObject = new int[] {1,0,0,1 } },
		new Observation{ CategoryId = 1, IsRelated = false, ObservedObject = new int[] {1,0,0,1 } },

		//13
		new Observation{ CategoryId = 1, IsRelated = false, ObservedObject = new int[] {1,1,0,0 } },

		//14
		new Observation{ CategoryId = 1, IsRelated = false, ObservedObject = new int[] {1,1,0,1 } },
		new Observation{ CategoryId = 1, IsRelated = false, ObservedObject = new int[] {1,1,0,1 } },
		new Observation{ CategoryId = 1, IsRelated = false, ObservedObject = new int[] {1,1,0,1 } },
	};

static IHostBuilder CreateHostBuilder(string[] args)
{
	return Host.CreateDefaultBuilder(args)
		.ConfigureServices((_, services) =>
			services
				.AddSingleton<IDistance, HammingDistance>()
				.AddSingleton<ICandidatesExtractor, MedoidBasedCandidates>()
				.AddSingleton<ICategoryDeterminer, BasicCategoryDeterminer>()
				.AddSingleton<ICategoryRepository, CategoryRepository>()
				.AddSingleton<IRelationsRepository, RelationsRepository>()
				.AddSingleton<IRelationsDeterminer, RelationsDeterminer>()
				.AddSingleton<IRelationFeaturesDeterminer, RelationFeaturesDeterminer>()
				//TODO Add interface
				.AddSingleton<ICategoryManager, CategoryManager.Manager.CategoryManager>());
}

using var host = CreateHostBuilder(args).Build();
Run(host.Services, observations);

static void Run(IServiceProvider services, Observation[] observations)
{
	using var serviceScope = services.CreateScope();
	var provider = serviceScope.ServiceProvider;

	var manager = provider.GetRequiredService<ICategoryManager>();

	//foreach (var obs in observations)
	//{
	//	manager.AddObservation(obs);
	//}

	var obsbatch = CategoryManager.Utils.ObservationsGenerator.GenerateObservations(1, new int[] { 1, 1, 1, 1, 0, 0, 0, 0 }, 2, 4, 20);

	manager.AddObservationsBatch(obsbatch);

	Console.WriteLine("sadasdasda");

	//manager.DisplaySummary();
}