using System.Collections.Generic;

public interface IHasContext
{
    IEnumerable<ContextAction> GetActions(BaseEntity entity);
}