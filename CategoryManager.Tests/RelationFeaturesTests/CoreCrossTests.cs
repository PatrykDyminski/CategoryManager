using CategoryManager.Distance;
using CategoryManager.Model;
using CategoryManager.Relations.Features;
using CategoryManager.Tests.Utils;
using CSharpFunctionalExtensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CategoryManager.Tests.RelationFeaturesTests;


[TestClass]
public class CoreCrossTests
{
	private static Func<CategorySummary, CategorySummary, Maybe<double>, Maybe<double>> TestMethod
		=> new RelationFeaturesDeterminer(new HammingDistance()).CoreCross;

	[TestMethod]
	public void CoreCrossBasicTest()
	{
		RFTUtils.PerformRFTest(8, 10, 8, 10, 13, true, 3, TestMethod);
	}

	[TestMethod]
	public void CoreCrossBasicTest2()
	{
		RFTUtils.PerformRFTest(8, 10, 4, 10, 13, false, Maybe.None, TestMethod);
	}

	[TestMethod]
	public void CoreCrossBasicTest3()
	{
		RFTUtils.PerformRFTest(10, 15, 3, 10, 13, false, Maybe.None, TestMethod);
	}
}
