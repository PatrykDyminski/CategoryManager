using CategoryManager.Model;

namespace CategoryManager.Tests.Utils;

public class CSUtils
{
	public static CategorySummary CreateSummary(double core, double boundary)
	{
		return new CategorySummary() { Tplus = core, Tminus = boundary };
	}
}
