using CSharpFunctionalExtensions;

namespace CategoryManager.Prediction;

internal interface ICategoryPredictor
{
  Result<PredictionResult> PredictCategory(int[] objectoToPredict, bool includeRelations);
}
