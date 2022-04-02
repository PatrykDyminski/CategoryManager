﻿using CategoryManager.Category;
using CategoryManager.Model;
using CategoryManager.Relations;
using CategoryManager.Repository.Interfaces;

namespace CategoryManager.Repository;

public class RelationsRepository : IRelationsRepository
{
	private readonly IRelationsDeterminer relationsDeterminer;

	private List<Relation> Relations;
	private Dictionary<int, CategorySummary> KnownCategories;

	public RelationsRepository(IRelationsDeterminer relationsDeterminer)
	{
		this.relationsDeterminer = relationsDeterminer;

		Relations = new List<Relation>();
		KnownCategories = new Dictionary<int, CategorySummary>();
	}

  public IReadOnlyCollection<Relation> GetAllRelations() => Relations;

  public IReadOnlyCollection<Relation> GetRelationsForCategory(int categoryId)
	{
		return Relations
			.Where(r => r.Cat1Id == categoryId || r.Cat2Id == categoryId)
			.ToList();
	}

	public void UpdateRelations(ICategory category)
	{
		if (IsNewCategory(category.Id))
		{
			HandleNewCategory(category.Id, category.Summary.Value);
		}
		else
		{
			HandleExistingRelationsForCategory(category.Id, category.Summary.Value);
		}
	}

	private void HandleExistingRelationsForCategory(int categoryId, CategorySummary categorySummary)
	{
		var part1 = Relations
			.Where(relation => relation.Cat1Id == categoryId)
			.Select(relation => new Relation
			{
				Cat1Id = categoryId,
				Cat2Id = relation.Cat2Id,
				CategorySummary1 = categorySummary,
				CategorySummary2 = relation.CategorySummary2,
				relationType = relation.relationType,
			})
			.Where(x => relationsDeterminer.ValidateRelation(x));

		var part2 = Relations
			.Where(relation => relation.Cat2Id == categoryId)
			.Select(relation => new Relation
			{
				Cat1Id = relation.Cat1Id,
				Cat2Id = categoryId,
				CategorySummary1 = relation.CategorySummary1,
				CategorySummary2 = categorySummary,
				relationType = relation.relationType,
			})
			.Where(x => relationsDeterminer.ValidateRelation(x));

		Relations = Relations
			.Where(relation => relation.Cat1Id != categoryId && relation.Cat2Id != categoryId)
			.Concat(part1)
			.Concat(part2)
			.ToList();
	}

	private void HandleNewCategory(int categoryId, CategorySummary categorySummary)
	{
		var listOfRelations = KnownCategories
			.SelectMany(x => relationsDeterminer
				.GetRelationsForCategories(categoryId, x.Key, categorySummary, x.Value));

		if (listOfRelations.Any())
		{
			Relations.AddRange(listOfRelations);
		}

		KnownCategories.Add(categoryId, categorySummary);
	}

	private bool IsNewCategory(int categoryId)
	{
		return !KnownCategories.ContainsKey(categoryId);
	}
}
