using CategoryManager.Macrostructure;
using CategoryManager.Model;
using CategoryManager.Relations.Determiner;
using CategoryManager.Relations.Features;
using CategoryManager.Relations.Types;
using CategoryManager.Tests.TestCategory;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace CategoryManager.Tests.RelationsRepositoryTests;

[TestClass]
public class SimilarityTests
{
  private static readonly HashSet<Observation> ObsCat1_1 = new()
  {
    new Observation { CategoryId = 1, IsRelated = true, ObservedObject = new int[] { 0, 0, 0, 0, 0 } },
    new Observation { CategoryId = 1, IsRelated = true, ObservedObject = new int[] { 0, 0, 0, 0, 1 } },
    new Observation { CategoryId = 1, IsRelated = true, ObservedObject = new int[] { 0, 0, 0, 1, 1 } },
    new Observation { CategoryId = 1, IsRelated = true, ObservedObject = new int[] { 0, 0, 1, 1, 1 } },
    new Observation { CategoryId = 1, IsRelated = true, ObservedObject = new int[] { 0, 1, 1, 1, 1 } },
    new Observation { CategoryId = 1, IsRelated = true, ObservedObject = new int[] { 1, 1, 1, 1, 1 } },
    new Observation { CategoryId = 1, IsRelated = true, ObservedObject = new int[] { 1, 1, 1, 1, 0 } },
    new Observation { CategoryId = 1, IsRelated = true, ObservedObject = new int[] { 1, 1, 1, 0, 0 } },
    new Observation { CategoryId = 1, IsRelated = true, ObservedObject = new int[] { 1, 1, 0, 0, 0 } },
    new Observation { CategoryId = 1, IsRelated = true, ObservedObject = new int[] { 1, 0, 0, 0, 0 } },
  };

  private static readonly HashSet<Observation> ObsCat2_1 = new()
  {
    new Observation { CategoryId = 2, IsRelated = true, ObservedObject = new int[] { 0, 0, 0, 0, 0 } },
    new Observation { CategoryId = 2, IsRelated = true, ObservedObject = new int[] { 0, 0, 0, 0, 1 } },
    new Observation { CategoryId = 2, IsRelated = true, ObservedObject = new int[] { 0, 0, 0, 1, 1 } },
    new Observation { CategoryId = 2, IsRelated = true, ObservedObject = new int[] { 0, 0, 1, 1, 1 } },
    new Observation { CategoryId = 2, IsRelated = true, ObservedObject = new int[] { 0, 1, 1, 1, 1 } },
    new Observation { CategoryId = 2, IsRelated = true, ObservedObject = new int[] { 1, 1, 1, 1, 1 } },
    new Observation { CategoryId = 2, IsRelated = true, ObservedObject = new int[] { 1, 1, 1, 1, 0 } },
    new Observation { CategoryId = 2, IsRelated = true, ObservedObject = new int[] { 1, 1, 1, 0, 0 } },
    new Observation { CategoryId = 2, IsRelated = true, ObservedObject = new int[] { 1, 1, 0, 0, 0 } },
    //new Observation { CategoryId = 2, IsRelated = true, ObservedObject = new int[] { 1, 0, 0, 0, 1 } },
  };

  private static readonly HashSet<Observation> ObsCat2_2 = new()
  {
    new Observation { CategoryId = 2, IsRelated = true, ObservedObject = new int[] { 0, 0, 0, 0, 0 } },
    new Observation { CategoryId = 2, IsRelated = true, ObservedObject = new int[] { 0, 0, 0, 0, 1 } },
    new Observation { CategoryId = 2, IsRelated = true, ObservedObject = new int[] { 0, 0, 0, 1, 1 } },
    new Observation { CategoryId = 2, IsRelated = true, ObservedObject = new int[] { 0, 0, 1, 1, 1 } },
    new Observation { CategoryId = 2, IsRelated = true, ObservedObject = new int[] { 0, 1, 1, 1, 1 } },
    new Observation { CategoryId = 2, IsRelated = true, ObservedObject = new int[] { 1, 1, 1, 1, 1 } },
    new Observation { CategoryId = 2, IsRelated = true, ObservedObject = new int[] { 1, 1, 1, 1, 0 } },
    new Observation { CategoryId = 2, IsRelated = true, ObservedObject = new int[] { 1, 1, 1, 0, 0 } },
    new Observation { CategoryId = 2, IsRelated = true, ObservedObject = new int[] { 1, 0, 0, 1, 0 } },
    new Observation { CategoryId = 2, IsRelated = true, ObservedObject = new int[] { 1, 0, 0, 0, 1 } },
  };

  private static readonly HashSet<Observation> ObsCat2_3 = new()
  {
    new Observation { CategoryId = 2, IsRelated = true, ObservedObject = new int[] { 0, 0, 0, 0, 0 } },
  };

  //Two sets have strong similarity

  [TestMethod]
  public void StrongSimilarityTest()
  {
    var sum1 = new CategoryMock(1, 5, 10, new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, ObsCat1_1, ObsCat1_1);
    var sum2 = new CategoryMock(2, 5, 10, new int[] { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, ObsCat2_1, ObsCat2_1);

    var rd = new RelationsDeterminer(new RelationFeaturesDeterminer(new HammingDistance()));

    var res = rd.DetermineSimilarityBasedOnObservationsSets(sum1, sum2);

    res.HasValue.Should().BeTrue();
    res.Value.Should().BeOfType<ObservationBasedSimilarity>();
  }

  //One set has weak similarity and one strong
  [TestMethod]
  public void NoStrongSimilarityTest_WeakSimilarity_BoundaryWeak()
  {
    var sum1 = new CategoryMock(1, 5, 10, new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, ObsCat1_1, ObsCat1_1);
    var sum2 = new CategoryMock(2, 5, 10, new int[] { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, ObsCat2_1, ObsCat2_2);

    var rd = new RelationsDeterminer(new RelationFeaturesDeterminer(new HammingDistance()));

    var res = rd.DetermineSimilarityBasedOnObservationsSets(sum1, sum2);

    res.HasValue.Should().BeTrue();
    res.Value.Should().BeOfType<ObservationBasedSimilarity>();
    var rel = (ISimilarityRelation) res.Value;
    rel.SimilarityLevel.Should().Be(SimilarityLevel.Weak);
  }


  //Two sets have weak similarity
  [TestMethod]
  public void WeakSimilarityTest()
  {
    var sum1 = new CategoryMock(1, 5, 10, new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, ObsCat1_1, ObsCat1_1);
    var sum2 = new CategoryMock(2, 5, 10, new int[] { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, ObsCat2_2, ObsCat2_2);

    var rd = new RelationsDeterminer(new RelationFeaturesDeterminer(new HammingDistance()));

    var res = rd.DetermineSimilarityBasedOnObservationsSets(sum1, sum2);

    res.HasValue.Should().BeTrue();
    res.Value.Should().BeOfType<ObservationBasedSimilarity>();
    var rel = (ISimilarityRelation)res.Value;
    rel.SimilarityLevel.Should().Be(SimilarityLevel.Weak);
  }

  //one sets have no similarity
  [TestMethod]
  public void NoSimilarityTest()
  {
    var sum1 = new CategoryMock(1, 5, 10, new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, ObsCat1_1, ObsCat1_1);
    var sum2 = new CategoryMock(2, 5, 10, new int[] { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, ObsCat2_2, ObsCat2_3);

    var rd = new RelationsDeterminer(new RelationFeaturesDeterminer(new HammingDistance()));

    var res = rd.DetermineSimilarityBasedOnObservationsSets(sum1, sum2);

    res.HasValue.Should().BeFalse();
  }
}
