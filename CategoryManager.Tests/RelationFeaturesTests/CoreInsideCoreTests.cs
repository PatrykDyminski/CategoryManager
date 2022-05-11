using CategoryManager.Macrostructure;
using CategoryManager.Model;
using CategoryManager.Relations.Features;
using CategoryManager.Tests.Utils;
using CSharpFunctionalExtensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CategoryManager.Tests.RelationFeaturesTests;

[TestClass]
public class CoreInsideCoreTests
{
	private static Func<CategorySummary, CategorySummary, Maybe<double>, Maybe<(double, int)>> TestMethod
		=> new RelationFeaturesDeterminer(new HammingDistance()).CoreInsideCore;

	[TestMethod]
	public void CoreInsideCoreBasicTest()
	{
		RFTUtils.PerformRFTest(10, 20, 2, 8, 5, true, 3, 1, TestMethod);
	}

	[TestMethod]
	public void CoreInsideCoreBasicTest2()
	{
		RFTUtils.PerformRFTest(10, 20, 6, 10, 5, false, Maybe.None, Maybe.None, TestMethod);
	}

	[TestMethod]
	public void CoreInsideCoreBasicTest3()
	{
		RFTUtils.PerformRFTest(10, 20, 5, 10, 5, false, Maybe.None, Maybe.None, TestMethod);
	}
}
