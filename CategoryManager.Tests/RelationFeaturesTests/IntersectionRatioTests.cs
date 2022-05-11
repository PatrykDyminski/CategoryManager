using CategoryManager.Macrostructure;
using CategoryManager.Model;
using CategoryManager.Relations.Features;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace CategoryManager.Tests.RelationFeaturesTests;

[TestClass]
public class IntersectionRatioTests
{
  [TestMethod]
  public void CoreIntersectionTest1()
  {
    //Arrange
    var rfd = new RelationFeaturesDeterminer(new HammingDistance());

    var set1 = new HashSet<Observation>() {
      new Observation() { ObservedObject = new int[] { 1, 0, 0 } },
      new Observation() { ObservedObject = new int[] { 1, 1, 0 } },
      new Observation() { ObservedObject = new int[] { 1, 1, 1 } },
    };

    var set2 = new HashSet<Observation>() {
      new Observation() { ObservedObject = new int[] { 1, 0, 0 } },
      new Observation() { ObservedObject = new int[] { 1, 1, 0 } },
      new Observation() { ObservedObject = new int[] { 1, 1, 1 } },
    };

    //Act
    var res = rfd.IntersectionRatio(set1, set2);

    //Assert
    res.Should().Be(1);
  }

  [TestMethod]
  public void CoreIntersectionTest2()
  {
    //Arrange
    var rfd = new RelationFeaturesDeterminer(new HammingDistance());

    var set1 = new HashSet<Observation>() {
      new Observation() { ObservedObject = new int[] { 1, 0, 0 } },
      new Observation() { ObservedObject = new int[] { 1, 1, 0 } },
      new Observation() { ObservedObject = new int[] { 1, 1, 1 } },
    };

    var set2 = new HashSet<Observation>() {
      new Observation() { ObservedObject = new int[] { 0, 1, 0 } },
    };

    //Act
    var res = rfd.IntersectionRatio(set1, set2);

    //Assert
    res.Should().Be(0);
  }

  [TestMethod]
  public void CoreIntersectionTest3()
  {
    //Arrange
    var rfd = new RelationFeaturesDeterminer(new HammingDistance());

    var set1 = new HashSet<Observation>() {
      new Observation() { ObservedObject = new int[] { 1, 0, 0 } },
      new Observation() { ObservedObject = new int[] { 1, 1, 0 } },
      new Observation() { ObservedObject = new int[] { 1, 1, 1 } },
      new Observation() { ObservedObject = new int[] { 0, 1, 1 } },
    };

    var set2 = new HashSet<Observation>() {
      new Observation() { ObservedObject = new int[] { 1, 0, 0 } },
      new Observation() { ObservedObject = new int[] { 1, 1, 0 } },
    };

    //Act
    double res = rfd.IntersectionRatio(set1, set2);

    //Assert
    res.Should().Be(0.5);
  }
}
