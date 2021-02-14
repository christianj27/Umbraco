using System.Web.Mvc;
using Umbraco.Web.Models;
using Umbraco.Web.Mvc;
using Umbraco.Models;
using Umbraco.Models.ViewModels;
using Umbraco.Services;
using System.Collections.Generic;

namespace Umbraco.Controller
{
    public class SearchPageController : RenderMvcController
    {
        private readonly ISearchService _searchService;
        private readonly IDataTypeValueService _dataTypeValueService;
        public string[] DocTypeAliases { get; set; }

        public SearchPageController(ISearchService searchService, IDataTypeValueService dataTypecValueService)
        {
            _searchService = searchService;
            _dataTypeValueService = dataTypecValueService;
            DocTypeAliases = new[] { "BlogPost", "blogEntries", "ArticulateMarkdown" };
        }

        public ActionResult Index(ContentModel model, string query, string page, string category)
        {
            var searchPageModel = new SearchContentModels(model.Content);

            IEnumerable<SelectListItem> categories = 
                _dataTypeValueService.GetItemsFromValueListDataType("[MULTICHECKBOXLIST] Category List", null);

            var searchViewModel = new SearchViewModel()
            {
                Query = query,
                Category = category,
                Page = page,
                Categories = categories
            };

            if(!int.TryParse(page, out var pageNumber))
            {
                pageNumber = 1;
            }

            var searchResults = _searchService.GetPageOfContentSearchResults(query, category,
                pageNumber, out var totalItemCount, DocTypeAliases);
            //var searchResults = _searchService.GetPageOfContentSearchResults(query, category,
            //    pageNumber, out var totalItemCount, null);

            searchPageModel.SearchViewModel = searchViewModel;
            searchPageModel.SearchResults = searchResults;

            return CurrentTemplate(searchPageModel);
        }
    }
}