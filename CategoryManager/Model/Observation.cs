namespace CategoryManager.Model;

public class Observation
{
	/// <summary>Id of the category</summary>
	public int CategoryId { get; set; }

	/// <summary>Is observation related with category. Otherwise false.</summary>
	public bool IsRelated { get; set; }

	/// <summary>Observed Object</summary>
	public int[] ObservedObject {  get; set; } = Array.Empty<int>();
}
