using CSharpFunctionalExtensions;

namespace CategoryManager.Utils;

public static class ExtensionMethods
{
	public static string AsString(this int[] array)
	{
		return string.Join("", array);
	}

	public static string AsString(this Maybe<int[]> array)
	{
		return array.GetValueOrDefault(x => string.Join("", x), "Empty Array");
	}
}
