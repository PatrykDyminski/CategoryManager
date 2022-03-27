using CategoryManager.Candidates;
using CategoryManager.CategoryDeterminer;
using CategoryManager.Distance;
using CategoryManager.Model;
using CategoryManager.Utils;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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

		category.Value.CoreObservations.Count.Should().Be(2);
		category.Value.BoundaryObservations.Count.Should().Be(3);

		category.Value.Summary.Prototype.AsString().Should().Be("0011");
	}
}
