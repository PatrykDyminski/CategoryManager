using CategoryManager.Utils;

namespace CategoryManager.Model;

public class CategorySummary
{
	public int[] Prototype { get; set; } = Array.Empty<int>();

	/// <summary>Core</summary>
	public double Tplus { get; set; }

	/// <summary>Boundary</summary>
	public double Tminus { get; set; }

	public override string ToString()
	{
		return "Tplus: " + Tplus + ", Tminus: " + Tminus + ", Prototype: " + Prototype.AsString();
	}
}
