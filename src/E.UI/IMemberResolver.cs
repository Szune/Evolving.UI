using System.Collections.Generic;
using E.UI.Utilities;

namespace E.UI
{
    public interface IMemberResolver
    {
        IEnumerable<EMemberInfo> GetMembers(object objectToEdit);
        bool HasLabel(EMemberInfo member);
        bool IsReadOnly(EMemberInfo member);
    }
}