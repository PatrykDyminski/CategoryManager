using CategoryManager.Distance;
using CategoryManager.Model;
using CategoryManager.Relations.Features;
using CategoryManager.Tests.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CategoryManager.Tests.RelationFeaturesTests;

[TestClass]
public class BoundaryCrossTests
{
	private static Func<CategorySummary, CategorySummary, double, (bool, double)> TestMethod 
		=> new RelationFeaturesDeterminer(new HammingDistance()).BoundaryCross;

	[TestMethod]
	public void BoundaryCrossBasicTest()
	{
		RFTUtils.PerformRFTest(4, 8, 4, 8, 13, true, 3, TestMethod);
	}

	[TestMethod]
	public void BoundaryCrossBasicTest2()
	{
		RFTUtils.PerformRFTest(2, 8, 2, 4, 13, false, -1, TestMethod);
	}

	[TestMethod]
	public void BoundaryCrossBasicTest3()
	{
		RFTUtils.PerformRFTest(5, 10, 1, 3, 13, false, -1, TestMethod);
	}
}
