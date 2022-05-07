using CategoryManager.Candidates;
using CategoryManager.Category.Factory;
using CategoryManager.CategoryDeterminer;
using CategoryManager.Distance;
using CategoryManager.Manager;
using CategoryManager.Relations.Determiner;
using CategoryManager.Relations.Features;
using CategoryManager.Relations.Validator;
using CategoryManager.Repository;
using CategoryManager.Repository.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

static IHostBuilder CreateHostBuilder(string[] args)
{
  return Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) =>
      services
        .AddSingleton<IDistance, HammingDistance>()
        .AddSingleton<ICandidatesExtractor, MedoidBasedCandidates>()
        .AddSingleton<ICategoryDeterminer, BasicCategoryDeterminer>()
        .AddTransient<IRelationValidator, RelationValidator>()
        .AddSingleton<ICategoryRepository, CategoryRepository>()
        .AddSingleton<IRelationsRepository, RelationsRepository>()
        .AddSingleton<IRelationsDeterminer, RelationsDeterminer>()
        .AddSingleton<IRelationFeaturesDeterminer, RelationFeaturesDeterminer>()
        .AddSingleton<ICategoryFactory, CategoryFactory>()
        .AddSingleton<ICategoryManager, CategoryManager.Manager.CategoryManager>());
}

using var host = CreateHostBuilder(args).Build();
using var serviceScope = host.Services.CreateScope();

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
