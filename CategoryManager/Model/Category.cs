namespace CategoryManager.Model;

public class Category
{
	public int[] Prototype { get; set; } = Array.Empty<int>();

	/// <summary>Core</summary>
	public double Tplus { get; set; }

	/// <summary>Boundary</summary>
	public double Tminus { get; set; }
}
