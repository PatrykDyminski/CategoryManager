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
		if(Prototype == Array.Empty<int>())
		{
			return "Empty summary";
		}

		return "Core: " + Tplus + ", Boundary: " + Tminus + ", Prototype: " + Prototype.AsString();
	}
}
