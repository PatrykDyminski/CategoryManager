using CategoryManager.Candidates;
using CategoryManager.Macrostructure;
using CategoryManager.Utils;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CategoryManager.Tests;

[TestClass]
public class CandidatesTests
{
	private static readonly int[][] universe = new int[][]
	{
		new int[] { 0, 0, 0},
		new int[] { 0, 0, 1},
		new int[] { 0, 1, 0},
		new int[] { 0, 1, 1},
		new int[] { 1, 0, 0},
		new int[] { 1, 0, 1},
		new int[] { 1, 1, 0},
		new int[] { 1, 1, 1},
	};

	private static readonly int[][] list = new int[][]
	{
		new int[] { 0, 0, 0},
		new int[] { 0, 1, 0},
		new int[] { 0, 1, 1},
		new int[] { 0, 1, 1},
		new int[] { 1, 0, 1},
		new int[] { 1, 0, 1},
	};

	[TestMethod]
	public void CentroidCandidatesTest()
	{
		var centroid = new CentroidBasedCandidates(new HammingDistance());
		var cands = centroid.ExtractCandidates(list, universe);

		cands.Should().HaveCount(2);
		cands[0].Length.Should().Be(3);
		cands[1].Length.Should().Be(3);

		cands[0].AsString().Should().Be("001");
		cands[1].AsString().Should().Be("011");
	}

	[TestMethod]
	public void MerdoidCandidatesTest()
	{
		var medoid = new MedoidBasedCandidates(new HammingDistance());
		var cands = medoid.ExtractCandidates(list);

		cands.Should().HaveCount(1);
		cands[0].Length.Should().Be(3);

		cands[0].AsString().Should().Be("011");
	}

}
