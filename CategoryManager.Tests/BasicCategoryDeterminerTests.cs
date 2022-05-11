using CategoryManager.Candidates;
using CategoryManager.CategoryDeterminer;
using CategoryManager.Macrostructure;
using CategoryManager.Model;
using CategoryManager.Utils;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace CategoryManager.Tests;

[TestClass]
public class BasicCategoryDeterminerTests
{
	private static readonly Observation[] observations = new Observation[]
	{
		//related
		//1
		new Observation{ CategoryId = 1, IsRelated = true, ObservedObject = new int[] {0,0,0,0 } },

		//3
		new Observation{ CategoryId = 1, IsRelated = true, ObservedObject = new int[] {0,0,1,0 } },

		//4
		new Observation{ CategoryId = 1, IsRelated = true, ObservedObject = new int[] {0,0,1,1 } },
		new Observation{ CategoryId = 1, IsRelated = true, ObservedObject = new int[] {0,0,1,1 } },

		//6
		new Observation{ CategoryId = 1, IsRelated = true, ObservedObject = new int[] {0,1,0,1 } },
		new Observation{ CategoryId = 1, IsRelated = true, ObservedObject = new int[] {0,1,0,1 } },

		//not related
		//6
		new Observation{ CategoryId = 1, IsRelated = false, ObservedObject = new int[] {0,1,0,1 } },

		//10
		new Observation{ CategoryId = 1, IsRelated = false, ObservedObject = new int[] {1,0,0,1 } },
		new Observation{ CategoryId = 1, IsRelated = false, ObservedObject = new int[] {1,0,0,1 } },

		//13
		new Observation{ CategoryId = 1, IsRelated = false, ObservedObject = new int[] {1,1,0,0 } },

		//14
		new Observation{ CategoryId = 1, IsRelated = false, ObservedObject = new int[] {1,1,0,1 } },
		new Observation{ CategoryId = 1, IsRelated = false, ObservedObject = new int[] {1,1,0,1 } },
		new Observation{ CategoryId = 1, IsRelated = false, ObservedObject = new int[] {1,1,0,1 } },
	};

	[TestMethod]
	public void BasicCategoryDeterminerTest()
	{
		var cate = new BasicCategoryDeterminer(new HammingDistance(), new MedoidBasedCandidates(new HammingDistance()));
		var category = cate.DetermineCategory(observations);

		category.IsSuccess.Should().BeTrue();
		category.Value.Summary.Tminus.Should().Be(3);
		category.Value.Summary.Tplus.Should().Be(1);

		var coreset = category.Value.CoreObservationSet.Select(x => x.ObservedObject.AsString()).ToHashSet();
		var boundaryset = category.Value.BoundaryObservationSet.Select(x => x.ObservedObject.AsString()).ToHashSet();

		coreset.Count.Should().Be(2);
		boundaryset.Count.Should().Be(3);

		coreset.Contains("0010").Should().BeTrue();
		coreset.Contains("0011").Should().BeTrue();

		boundaryset.Contains("0000").Should().BeTrue();
		boundaryset.Contains("0101").Should().BeTrue();
		boundaryset.Contains("1001").Should().BeTrue();

		category.Value.Summary.Prototype.AsString().Should().Be("0011");
	}
}
