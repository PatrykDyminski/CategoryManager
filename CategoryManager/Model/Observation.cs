namespace CategoryManager.Model;

internal class Observation
{
	public int CategoryId { get; set; }

	public bool IsRelated { get; set; }

	public int[] ObservedObject {  get; set; } = Array.Empty<int>();
}
