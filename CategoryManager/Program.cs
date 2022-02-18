using CategoryManager.Distance;

var dst = new HammingDistance();
var dst2 = new JaccardDistance();

int[] obj1 = { 0, 1, 0, 1, 0, 1 };
int[] obj2 = { 0, 0, 0, 1, 1, 1 };

Console.WriteLine(dst.CalculateDistance(obj1, obj2));
Console.WriteLine(dst2.CalculateDistance(obj1, obj2));

