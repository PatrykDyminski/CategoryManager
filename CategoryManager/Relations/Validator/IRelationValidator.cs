using CategoryManager.Relations.Types;

namespace CategoryManager.Relations.Validator;

public interface IRelationValidator
{
  bool ValidateRelation(IRelation relation);
}
