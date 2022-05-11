using CategoryManager.Candidates;
using CategoryManager.Category.Factory;
using CategoryManager.CategoryDeterminer;
using CategoryManager.Macrostructure;
using CategoryManager.Model;
using CategoryManager.Relations.Determiner;
using CategoryManager.Relations.Features;
using CategoryManager.Relations.Types;
using CategoryManager.Relations.Validator;
using CategoryManager.Repository;
using CategoryManager.Repository.Interfaces;
using CategoryManager.Tests.Mocks;
using CategoryManager.Tests.TestCategory;
using CategoryManager.Tests.Utils;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace CategoryManager.Tests.RelationsRepositoryTests;

[TestClass]
public class RelationsRepositoryAddingTests
{
	private readonly IRelationsDeterminer relDet;

	private static readonly ISet<Observation> EmptySet = new HashSet<Observation>();

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
		var catRepo = new CategoryRepository(new CategoryFactory( new HammingDistance(), new BasicCategoryDeterminer(new HammingDistance(), new MedoidBasedCandidates(new HammingDistance()))));
		IRelationsRepository relationsRepository = new RelationsRepository(relDet, catRepo, new RelationValidator(catRepo, relDet));

		CategorySummary sum1 = CSUtils.CreateSummary(1, 3, new int[] { 0, 0, 0, 0 });
		CategoryMock category = new(1, sum1, EmptySet, EmptySet);

		//Act
		relationsRepository.UpdateRelations(category);

		//Assert
		relationsRepository.GetRelationsForCategory(1).Should().BeEmpty();
	}

	[TestMethod]
	public void Adding2NewCategories_OneRelation_Specification()
	{
		//Arrange
		var catRepo = new CategoryRepositoryMock();
		IRelationsRepository relationsRepository = new RelationsRepository(relDet, catRepo, new RelationValidator(catRepo, relDet));

		CategorySummary sum1 = CSUtils.CreateSummary(1, 3, new int[] { 0, 0, 0, 0 });
		CategorySummary sum2 = CSUtils.CreateSummary(2, 6, new int[] { 0, 0, 0, 0 });

		var cat1Id = 100;
		var cat2Id = 200;

		CategoryMock category1 = new(cat1Id, sum1, EmptySet, EmptySet);
		CategoryMock category2 = new(cat2Id, sum2, EmptySet, EmptySet);

		//Act
		catRepo.AddCategory(category1);
		relationsRepository.UpdateRelations(category1);

		catRepo.AddCategory(category2);
		relationsRepository.UpdateRelations(category2);

		//Assert
		var rels1 = relationsRepository.GetRelationsForCategory(cat1Id);
		var rels2 = relationsRepository.GetRelationsForCategory(cat2Id);

		rels1.Should().HaveCount(1);
		rels2.Should().HaveCount(1);

		rels1.ToList()[0].Should().BeOfType<SpecificationRelation>();
		rels2.ToList()[0].Should().BeOfType<SpecificationRelation>();
	}

	[TestMethod]
	public void Adding2NewCategories_ThenUpdate_OneRelation_ThenZero()
	{
		//Arrange
		var catRepo = new CategoryRepository(new CategoryFactory(new HammingDistance(), new BasicCategoryDeterminer(new HammingDistance(), new MedoidBasedCandidates(new HammingDistance()))));
		IRelationsRepository relationsRepository = new RelationsRepository(relDet, catRepo, new RelationValidator(catRepo, relDet));

		CategorySummary sum1 = CSUtils.CreateSummary(1, 3, new int[] { 0, 0, 0, 0 });
		CategorySummary sum2 = CSUtils.CreateSummary(2, 6, new int[] { 0, 0, 0, 0 });

		CategorySummary sum1_2 = CSUtils.CreateSummary(1, 7, new int[] { 1, 1, 1, 1 });

		CategoryMock category1 = new(1, sum1, EmptySet, EmptySet);
		CategoryMock category2 = new(2, sum2, EmptySet, EmptySet);

		CategoryMock category1_2 = new(1, sum1_2, EmptySet, EmptySet);

		//Act
		relationsRepository.UpdateRelations(category1);
		relationsRepository.UpdateRelations(category2);

		relationsRepository.UpdateRelations(category1_2);

		//Assert
		var rels1 = relationsRepository.GetRelationsForCategory(1);
		var rels2 = relationsRepository.GetRelationsForCategory(2);

		rels1.Should().HaveCount(0);
		rels2.Should().HaveCount(0);
	}

	[TestMethod]
	public void Adding2NewCategories_ThenUpdateOneCategoryPrototype_RelationPersists()
	{
		//Arrange
		var catRepo = new CategoryRepositoryMock();
		IRelationsRepository relationsRepository = new RelationsRepository(relDet, catRepo, new RelationValidator(catRepo, relDet));

		CategorySummary sum1 = CSUtils.CreateSummary(1, 3, new int[] { 0, 0, 0, 0 });
		CategorySummary sum2 = CSUtils.CreateSummary(3, 6, new int[] { 0, 0, 0, 0 });

		CategorySummary sum1_2 = CSUtils.CreateSummary(1, 4, new int[] { 1, 0, 0, 0 });

		CategoryMock category1 = new(100, sum1, EmptySet, EmptySet);
		CategoryMock category2 = new(200, sum2, EmptySet, EmptySet);

		CategoryMock category1_2 = new(100, sum1_2, EmptySet, EmptySet);

		//Act
		catRepo.AddCategory(category1);
		relationsRepository.UpdateRelations(category1);

		catRepo.AddCategory(category2);
		relationsRepository.UpdateRelations(category2);

		catRepo.AddCategory(category1_2);

		relationsRepository.UpdateRelations(category1_2);

		//Assert
		var rels1 = relationsRepository.GetRelationsForCategory(100);
		var rels2 = relationsRepository.GetRelationsForCategory(200);

		rels1.Should().HaveCount(1);
		rels2.Should().HaveCount(1);
	}
}
