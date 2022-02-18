using CategoryManager.Distance;

var dst = new HammingDistance();

int[] obj1 = { 1, 0, 0, 1 };
int[] obj2 = { 1, 0, 1, 0 };

Console.WriteLine(dst.CalculateDistance(obj1, obj2));

