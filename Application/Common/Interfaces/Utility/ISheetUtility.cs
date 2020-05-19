using System.Collections.Generic;
using System.Data;

namespace Application.Common.Interfaces
{
    public interface ISheetUtility
    {
        Dictionary<string, int> MatchRequiredColumn(DataTable table, IEnumerable<string> requiredColumns);
    }
}
