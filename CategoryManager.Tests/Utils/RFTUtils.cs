using CategoryManager.Model;
using CSharpFunctionalExtensions;
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
		Maybe<double> expectedNumericResult,
		Func<CategorySummary, CategorySummary, Maybe<double>, Maybe<double>> method)
	{
		var c1 = CSUtils.CreateSummary(core1, boundary1);
		var c2 = CSUtils.CreateSummary(core2, boundary2);
		var result = method(c1, c2, distance);
		result.HasValue.Should().Be(expectedResult);

		if (expectedResult)
		{
			result.Value.Should().Be(expectedNumericResult.Value);
		}
	}

	public static void PerformRFTest(
	double core1,
	double boundary1,
	double core2,
	double boundary2,
	double distance,
	bool expectedResult,
	Maybe<double> expectedNumericResult,
	Maybe<int> expectedBiggerResult,
	Func<CategorySummary, CategorySummary, Maybe<double>, Maybe<(double, int)>> method)
	{
		var c1 = CSUtils.CreateSummary(core1, boundary1);
		var c2 = CSUtils.CreateSummary(core2, boundary2);
		var result = method(c1, c2, distance);
		result.HasValue.Should().Be(expectedResult);

		if (expectedResult)
		{
			result.Value.Item1.Should().Be(expectedNumericResult.Value);
			result.Value.Item2.Should().Be(expectedBiggerResult.Value);
		}
	}
}
