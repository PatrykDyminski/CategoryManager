using CategoryManager.Macrostructure;
using CategoryManager.Model;
using CategoryManager.Relations.Features;
using CategoryManager.Tests.Utils;
using CSharpFunctionalExtensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CategoryManager.Tests.RelationFeaturesTests;

[TestClass]
public class BoundaryInsideBoundaryTests
{
	private static Func<CategorySummary, CategorySummary, Maybe<double>, Maybe<(double, int)>> TestMethod
		=> new RelationFeaturesDeterminer(new HammingDistance()).BoundaryInsideBoundary;

	[TestMethod]
	public void BoundaryInsideBoundaryBasicTest()
	{
		RFTUtils.PerformRFTest(5, 10, 1, 2, 5, true, 3, 1, TestMethod);
	}

	[TestMethod]
	public void BoundaryInsideBoundaryBasicTest2()
	{
		RFTUtils.PerformRFTest(1, 10, 3, 6, 5, false, Maybe.None, Maybe.None, TestMethod);
	}

	[TestMethod]
	public void BoundaryInsideBoundaryBasicTest3()
	{
		RFTUtils.PerformRFTest(5, 10, 2, 5, 5, false, Maybe.None, Maybe.None, TestMethod);
	}
}
