using CategoryManager.Model;
using System;

namespace CategoryManager.Tests.Utils;

public class CSUtils
{
	public static CategorySummary CreateSummary(double core, double boundary, int[] prototype = null)
	{
		return new CategorySummary() { Tplus = core, Tminus = boundary, Prototype = prototype};
	}
}
