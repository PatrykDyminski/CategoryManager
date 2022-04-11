﻿using CSharpFunctionalExtensions;

namespace CategoryManager.Relations;

public class IRelation
{
  public int Cat1Id { get; init; }
  public int Cat2Id { get; init; }
  public Maybe<int> BiggerCategoryId { get; init; } = Maybe.None;
}
