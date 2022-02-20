using CategoryManager.Distance;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CategoryManager.Tests;

[TestClass]
public class DistanceTests
{
	[TestMethod]
	public void HammingDistanceOtherTest()
	{
		var hamming = new HammingDistance();

		int[] obj1 = { 0, 1, 0, 1, 0, 1 };
		int[] obj2 = { 0, 0, 0, 1, 1, 1 };

		var distance = hamming.CalculateDistance(obj1, obj2);

		distance.Should().Be(2);
	}

	[TestMethod]
	public void HammingDistanceSameTest()
	{
		var hamming = new HammingDistance();

		int[] obj1 = { 0, 0, 0, 1, 1, 1 };
		int[] obj2 = { 0, 0, 0, 1, 1, 1 };

		var distance = hamming.CalculateDistance(obj1, obj2);

		distance.Should().Be(0);
	}

	[TestMethod]
	public void HammingDistanceWrongLengthTest()
	{
		var hamming = new HammingDistance();

		int[] obj1 = { 0, 0, 0, 1, 1 };
		int[] obj2 = { 0, 0, 0, 1, 1, 1 };

		Action act = () => hamming.CalculateDistance(obj1, obj2);

		act.Should()
			.Throw<ArgumentException>()
			.WithMessage("Objects must be equal length");
	}

	[TestMethod]
	public void JaccardDistanceTest()
	{
		var jaccard = new JaccardDistance();

		int[] obj1 = { 0, 1, 0, 1, 0, 1 };
		int[] obj2 = { 0, 0, 0, 1, 1, 1 };

		var distance = jaccard.CalculateDistance(obj1, obj2);

		distance.Should().Be(0.5);
	}
}
