using CategoryManager.Category;
using CategoryManager.Model;
using CSharpFunctionalExtensions;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace CategoryManager.Tests.TestCategory;

public class CategoryMock : ICategory
{
  private readonly int id;
  private readonly CategorySummary summary;
  private readonly ISet<Observation>? coreObservations;
  private readonly ISet<Observation>? boundaryObservarions;

  public CategoryMock(int id, CategorySummary summary, ISet<Observation> coreObservations, ISet<Observation> boundaryObservarions)
  {
    this.id = id;
    this.summary = summary;
    this.coreObservations = coreObservations;
    this.boundaryObservarions = boundaryObservarions;
  }

  public CategoryMock(int id, double core, double boundary, int[] prototype = null)
  {
    this.id = id;
    summary = new CategorySummary() { Tplus = core, Tminus = boundary, Prototype = prototype };
  }

  public int Id => id;

  public Maybe<CategorySummary> Summary => summary;

  public bool AddObservation(Observation observation)
  {
    throw new System.NotImplementedException();
  }

  public void DisplayCategorySummary()
  {
    throw new System.NotImplementedException();
  }

  public Maybe<ImmutableHashSet<Observation>> GetCoreObservations()
  {
    return coreObservations.ToImmutableHashSet();
  }

  public Maybe<ImmutableHashSet<Observation>> GetPositiveBoundaryObservations()
  {
    return boundaryObservarions.ToImmutableHashSet();
  }
}
