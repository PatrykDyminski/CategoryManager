using CategoryManager.Model;
using FluentAssertions;
using System;

namespace CategoryManager.Tests.Utils;

public class RFTUtils
{
	public static void PerformRFTest(
		double core1, 
		double boundary1, 
		double core2, 
		double boundary2, 
		double distance, 
		bool expectedResult, 
		double numericResult,
		Func<CategorySummary, CategorySummary, double, (bool result, double common)> method)
	{
		var c1 = CSUtils.CreateSummary(core1, boundary1);
		var c2 = CSUtils.CreateSummary(core2, boundary2);
		var (result, common) = method(c1, c2, distance);
		result.Should().Be(expectedResult);
		common.Should().Be(numericResult);
	}
}
