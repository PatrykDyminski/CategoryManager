using CategoryManager.Macrostructure;
using CategoryManager.Relations.Determiner;
using CategoryManager.Relations.Features;
using CategoryManager.Tests.TestCategory;
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
		var sum1 = new CategoryMock(1, 1, 3, new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 });
		var sum2 = new CategoryMock(2, 5, 10, new int[] { 1, 1, 0, 0, 0, 0, 0, 0, 0, 0 });

		var rd = new RelationsDeterminer(new RelationFeaturesDeterminer(new HammingDistance()));

		var res = rd.DetermineSpecification(sum1, sum2);

		res.HasValue.Should().BeTrue();
	}

	[TestMethod]
	public void SRTest2()
	{
		var sum1 = new CategoryMock(1, 1, 3, new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 });
		var sum2 = new CategoryMock(2, 5, 10, new int[] { 1, 1, 1, 1, 0, 0, 0, 0, 0, 0 });

		var rd = new RelationsDeterminer(new RelationFeaturesDeterminer(new HammingDistance()));

		var res = rd.DetermineSpecification(sum1, sum2);

		res.HasValue.Should().BeFalse();
	}

	[TestMethod]
	public void SRTest3()
	{
		var sum1 = new CategoryMock(1, 1, 11, new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 });
		var sum2 = new CategoryMock(2, 5, 10, new int[] { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0 });

		var rd = new RelationsDeterminer(new RelationFeaturesDeterminer(new HammingDistance()));

		var res = rd.DetermineSpecification(sum1, sum2);

		res.HasValue.Should().BeFalse();
	}

	[TestMethod]
	public void SRTest4()
	{
		var sum1 = new CategoryMock(1, 2, 6, new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 });
		var sum2 = new CategoryMock(2, 1, 10, new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 });

		var rd = new RelationsDeterminer(new RelationFeaturesDeterminer(new HammingDistance()));

		var res = rd.DetermineSpecification(sum1, sum2);

		res.HasValue.Should().BeFalse();
	}

}
