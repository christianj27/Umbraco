using System.Collections.Generic;
using System.Web.Mvc;

namespace Umbraco.Services
{
    public interface IDataTypeValueService
    {
        IEnumerable<SelectListItem> GetItemsFromValueListDataType(string dataTypeName, string[] selectedValue);
    }
}