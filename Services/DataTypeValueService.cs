using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Umbraco.Core.PropertyEditors;
using Umbraco.Core.Services;

namespace Umbraco.Services
{
    public class DataTypeValueService : IDataTypeValueService
    {
        private readonly IDataTypeService _dataTypeService;

        public DataTypeValueService(IDataTypeService dataTypeService)
        {
            _dataTypeService = dataTypeService;
        }

        public IEnumerable<SelectListItem> GetItemsFromValueListDataType(string dataTypeName, string[] selectedValue)
        {
            IEnumerable<SelectListItem> items = null;

            var dataTypeConfig = (ValueListConfiguration)_dataTypeService.GetDataType(dataTypeName).Configuration;

            if(dataTypeConfig.Items != null && dataTypeConfig.Items.Any())
            {
                items = dataTypeConfig.Items.Select(x => new SelectListItem()
                {
                    Text = x.Value,
                    Value = x.Value,
                    Selected = selectedValue != null && selectedValue.Contains(x.Value)
                });
            }

            return items;
        }
    }
}