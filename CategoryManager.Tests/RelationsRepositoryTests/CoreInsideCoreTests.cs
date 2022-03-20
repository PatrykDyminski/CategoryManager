using CategoryManager.Distance;
using CategoryManager.Model;
using CategoryManager.Relations;
using CategoryManager.Relations.Features;
using CategoryManager.Repository;
using CategoryManager.Repository.Interfaces;
using CategoryManager.Tests.Utils;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace CategoryManager.Tests.RelationsRepositoryTests;

[TestClass]
public class RelationsRepositoryAddingTests
{
	private readonly IRelationsDeterminer relDet;

	public RelationsRepositoryAddingTests()
	{
		relDet = 
			new RelationsDeterminer(
				new RelationFeaturesDeterminer(
					new HammingDistance()));
	}

	[TestMethod]
	public void AddingNewCategory()
	{
		//Arrange
		IRelationsRepository relationsRepository = new RelationsRepository(relDet);

		CategorySummary sum1 = CSUtils.CreateSummary(1, 3, new int[] { 0, 0, 0, 0 });
		CategorySummary sum2 = CSUtils.CreateSummary(2, 6, new int[] { 0, 0, 0, 0 });

		//Act
		relationsRepository.UpdateRelations(1, sum1);

		//Assert
		relationsRepository.GetRelationsForCategory(1).Should().BeEmpty();
	}

	[TestMethod]
	public void Adding2NewCategories_OneRelation_Specification()
	{
		//Arrange
		IRelationsRepository relationsRepository = new RelationsRepository(relDet);

		CategorySummary sum1 = CSUtils.CreateSummary(1, 3, new int[] { 0, 0, 0, 0 });
		CategorySummary sum2 = CSUtils.CreateSummary(2, 6, new int[] { 0, 0, 0, 0 });

		//Act
		relationsRepository.UpdateRelations(1, sum1);
		relationsRepository.UpdateRelations(2, sum2);

		//Assert
		var rels1 = relationsRepository.GetRelationsForCategory(1);
		var rels2 = relationsRepository.GetRelationsForCategory(2);

		rels1.Should().HaveCount(1);
		rels2.Should().HaveCount(1);

		rels1.ToList()[0].relationType.Should().Be(RelationType.Specification);
		rels2.ToList()[0].relationType.Should().Be(RelationType.Specification);
	}

	[TestMethod]
	public void Adding2NewCategories_ThenUpdate_OneRelation_ThenZero()
	{
		//Arrange
		IRelationsRepository relationsRepository = new RelationsRepository(relDet);

		CategorySummary sum1 = CSUtils.CreateSummary(1, 3, new int[] { 0, 0, 0, 0 });
		CategorySummary sum2 = CSUtils.CreateSummary(2, 6, new int[] { 0, 0, 0, 0 });

		CategorySummary sum1_2 = CSUtils.CreateSummary(1, 7, new int[] { 1, 1, 1, 1 });

		//Act
		relationsRepository.UpdateRelations(1, sum1);
		relationsRepository.UpdateRelations(2, sum2);

		relationsRepository.UpdateRelations(1, sum1_2);

		//Assert
		var rels1 = relationsRepository.GetRelationsForCategory(1);
		var rels2 = relationsRepository.GetRelationsForCategory(2);

		rels1.Should().HaveCount(0);
		rels2.Should().HaveCount(0);
	}

	[TestMethod]
	public void Adding2NewCategories_ThenUpdateOneRelationPrototype_RelationPersists()
	{
		//Arrange
		IRelationsRepository relationsRepository = new RelationsRepository(relDet);

		CategorySummary sum1 = CSUtils.CreateSummary(1, 3, new int[] { 0, 0, 0, 0 });
		CategorySummary sum2 = CSUtils.CreateSummary(3, 6, new int[] { 0, 0, 0, 0 });

		CategorySummary sum1_2 = CSUtils.CreateSummary(1, 4, new int[] { 1, 0, 0, 0 });

		//Act
		relationsRepository.UpdateRelations(1, sum1);
		relationsRepository.UpdateRelations(2, sum2);

		relationsRepository.UpdateRelations(1, sum1_2);

		//Assert
		var rels1 = relationsRepository.GetRelationsForCategory(1);
		var rels2 = relationsRepository.GetRelationsForCategory(2);

		rels1.Should().HaveCount(1);
		rels2.Should().HaveCount(1);

		var proto1 = rels1.ToList()[0].CategorySummary1.Prototype;
		var proto2 = rels1.ToList()[0].CategorySummary2.Prototype;

		proto1.Should().BeEquivalentTo(new int[] { 0, 0, 0, 0 });
		proto2.Should().BeEquivalentTo(new int[] { 1, 0, 0, 0 });
	}
}
