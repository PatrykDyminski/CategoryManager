using CategoryManager.Category;
using CategoryManager.Model;
using CategoryManager.Repository.Interfaces;
using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CategoryManager.Tests.Mocks;

internal class CategoryRepositoryMock : ICategoryRepository
{
  public List<ICategory> Categories;

  public CategoryRepositoryMock()
  {
    Categories = new List<ICategory>();
  }
  
  public void AddCategory(ICategory category){
    Categories.RemoveAll(c => c.Id == category.Id);
    Categories.Add(category);
  }

  public bool AddObservation(Observation observation)
  {
    throw new NotImplementedException();
  }

  public Result<ICategory> GetCategoryById(int id)
  {
    var cat = Categories
      .SingleOrDefault(x => x.Id == id);

    return cat != null
      ? Result.Success(cat)
      : Result.Failure<ICategory>("No such category");
  }

  public Result<CategorySummary> GetCategorySummaryById(int id)
  {
    throw new NotImplementedException();
  }

  public List<string> GetAllStringSummaries()
  {
    throw new NotImplementedException();
  }

  public List<ICategory> GetAllCategories()
  {
    return Categories;
  }
}
