namespace CategoryManager.Utils;

public static class ExtensionMethods
{
	public static string AsString(this int[] array)
	{
		return string.Join("", array);
	}
}
