using CategoryManager.Candidates;
using CategoryManager.CategoryDeterminer;
using CategoryManager.Macrostructure;
using CategoryManager.Model;
using CategoryManager.Utils;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace CategoryManager.Tests;

[TestClass]
public class CategoryTests
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
	public void BasicCategoryTest()
	{
		var catdet = new BasicCategoryDeterminer(new HammingDistance(), new MedoidBasedCandidates(new HammingDistance()));

		var category = new Category.Category(catdet, new HammingDistance(), 1);

		foreach (var obs in observations)
		{
			category.AddObservation(obs);
		}

		var res = category.Summary;

		res.Value.Tminus.Should().Be(3);
		res.Value.Tplus.Should().Be(1);

		category.Prototype.AsString().Should().Be("0011");
		res.Value.Prototype.AsString().Should().Be("0011");
	}
}
