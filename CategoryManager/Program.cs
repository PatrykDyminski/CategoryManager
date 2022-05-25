using CategoryManager.Candidates;
using CategoryManager.Category.Factory;
using CategoryManager.Category.Recalculation;
using CategoryManager.CategoryDeterminer;
using CategoryManager.Macrostructure;
using CategoryManager.Manager;
using CategoryManager.Prediction;
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
        .AddSingleton<ICategoryRecalculationDeterminer>(new RecalculationBasedOnProportion(0.15f))
        .AddSingleton<ICategoryDeterminer, BasicCategoryDeterminer>()
        .AddTransient<IRelationValidator, RelationValidator>()
        .AddSingleton<ICategoryRepository, CategoryRepository>()
        .AddSingleton<IRelationsRepository, RelationsRepository>()
        .AddSingleton<IRelationsDeterminer, RelationsDeterminer>()
        .AddSingleton<IRelationFeaturesDeterminer, RelationFeaturesDeterminer>()
        .AddSingleton<ICategoryFactory, CategoryFactory>()
        .AddSingleton<ICategoryPredictor, CategoryPredictor>()
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

var obsbatch = CategoryManager.Utils.ObservationsGenerator.GenerateObservations(1, new int[] { 1, 1, 1, 1, 0, 0, 0, 0 }, 2, 4, 30000);
manager.AddObservationsBatch(obsbatch);

var obsbatch2 = CategoryManager.Utils.ObservationsGenerator.GenerateObservations(2, new int[] { 1, 1, 1, 1, 0, 0, 0, 0 }, 2, 4, 30);
manager.AddObservationsBatch(obsbatch2);

var obsbatch3 = CategoryManager.Utils.ObservationsGenerator.GenerateObservations(3, new int[] { 1, 1, 1, 1, 0, 0, 0, 0 }, 2, 4, 30);
manager.AddObservationsBatch(obsbatch3);

var obsbatch4 = CategoryManager.Utils.ObservationsGenerator.GenerateObservations(4, new int[] { 1, 1, 1, 1, 0, 0, 0, 0 }, 2, 4, 30);
manager.AddObservationsBatch(obsbatch4);

var obsbatch5 = CategoryManager.Utils.ObservationsGenerator.GenerateObservations(5, new int[] { 1, 1, 1, 1, 0, 0, 0, 0 }, 2, 4, 30);
manager.AddObservationsBatch(obsbatch5);

Console.WriteLine(manager.GetCategorySummary(1).Value);
Console.WriteLine(manager.GetCategorySummary(2).Value);
Console.WriteLine(manager.GetCategorySummary(3).Value);
Console.WriteLine(manager.GetCategorySummary(4).Value);
Console.WriteLine(manager.GetCategorySummary(5).Value);

var predictor = provider.GetRequiredService<ICategoryPredictor>();

var predRes = predictor.PredictCategory(new int[] { 1, 1, 1, 1, 0, 0, 0, 0 }, true);

Console.WriteLine(predRes.Value.ClosestCategory.Id);
Console.WriteLine(predRes.Value.IsInCore);
predRes.Value.Relations.GetValueOrDefault().ForEach(x => Console.WriteLine(x.ToString()));