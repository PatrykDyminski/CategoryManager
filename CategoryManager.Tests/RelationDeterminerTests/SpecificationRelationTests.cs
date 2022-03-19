using CategoryManager.Distance;
using CategoryManager.Relations;
using CategoryManager.Relations.Features;
using CategoryManager.Tests.Utils;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CategoryManager.Tests.RelationDeterminerTests;


// TODO: Refactor this class
[TestClass]
public class SpecificationRelationTests
{

	[TestMethod]
	public void SRTest1()
	{
		var sum1 = CSUtils.CreateSummary(1, 3, new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 });
		var sum2 = CSUtils.CreateSummary(5, 10, new int[] { 1, 1, 0, 0, 0, 0, 0, 0, 0, 0 });

		var rd = new RelationsDeterminer(new RelationFeaturesDeterminer(new HammingDistance()));

		var res = rd.DetermineSpecification(sum1, sum2);

		res.Should().BeTrue();
	}

	[TestMethod]
	public void SRTest2()
	{
		var sum1 = CSUtils.CreateSummary(1, 3, new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 });
		var sum2 = CSUtils.CreateSummary(5, 10, new int[] { 1, 1, 1, 1, 0, 0, 0, 0, 0, 0 });

		var rd = new RelationsDeterminer(new RelationFeaturesDeterminer(new HammingDistance()));

		var res = rd.DetermineSpecification(sum1, sum2);

		res.Should().BeFalse();
	}

	[TestMethod]
	public void SRTest3()
	{
		var sum1 = CSUtils.CreateSummary(1, 11, new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 });
		var sum2 = CSUtils.CreateSummary(5, 10, new int[] { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0 });

		var rd = new RelationsDeterminer(new RelationFeaturesDeterminer(new HammingDistance()));

		var res = rd.DetermineSpecification(sum1, sum2);

		res.Should().BeFalse();
	}

	[TestMethod]
	public void SRTest4()
	{
		var sum1 = CSUtils.CreateSummary(2, 6, new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 });
		var sum2 = CSUtils.CreateSummary(1, 10, new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 });

		var rd = new RelationsDeterminer(new RelationFeaturesDeterminer(new HammingDistance()));

		var res = rd.DetermineSpecification(sum1, sum2);

		res.Should().BeFalse();
	}

}
