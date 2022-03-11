using CategoryManager.Distance;
using CategoryManager.Model;
using CategoryManager.Relations.Features;
using CategoryManager.Tests.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CategoryManager.Tests.RelationFeaturesTests;

[TestClass]
public class BoundaryInsideBoundaryTests
{
	private static Func<CategorySummary, CategorySummary, double, (bool, double)> TestMethod
		=> new RelationFeaturesDeterminer(new HammingDistance()).BoundaryInsideBoundary;

	[TestMethod]
	public void BoundaryInsideBoundaryBasicTest()
	{
		RFTUtils.PerformRFTest(5, 10, 1, 2, 5, true, 3, TestMethod);
	}

	[TestMethod]
	public void BoundaryInsideBoundaryBasicTest2()
	{
		RFTUtils.PerformRFTest(1, 10, 3, 6, 5, false, -1, TestMethod);
	}

	[TestMethod]
	public void BoundaryInsideBoundaryBasicTest3()
	{
		RFTUtils.PerformRFTest(5, 10, 2, 5, 5, false, -1, TestMethod);
	}
}
