using CategoryManager.Distance;
using CategoryManager.Model;
using CategoryManager.Relations.Features;
using CategoryManager.Tests.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CategoryManager.Tests.RelationFeaturesTests;

[TestClass]
public class CoreInsideCoreTests
{
	private static Func<CategorySummary, CategorySummary, double, (bool, double)> TestMethod
		=> new RelationFeaturesDeterminer(new HammingDistance()).CoreInsideCore;

	[TestMethod]
	public void CoreInsideCoreBasicTest()
	{
		RFTUtils.PerformRFTest(10, 20, 2, 8, 5, true, 3, TestMethod);
	}

	[TestMethod]
	public void CoreInsideCoreBasicTest2()
	{
		RFTUtils.PerformRFTest(10, 20, 6, 10, 5, false, -1, TestMethod);
	}

	[TestMethod]
	public void CoreInsideCoreBasicTest3()
	{
		RFTUtils.PerformRFTest(10, 20, 5, 10, 5, false, -1, TestMethod);
	}
}
