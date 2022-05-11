namespace CategoryManager.Macrostructure;

public interface IDistance : IMacrostructure
{
  double CalculateDistance(int[] object1, int[] object2);
}
