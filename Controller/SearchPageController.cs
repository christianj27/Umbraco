using System.Web.Mvc;
using Umbraco.Web.Models;
using Umbraco.Web.Mvc;
using Umbraco.Models;
using Umbraco.Models.ViewModels;
using Umbraco.Services;

namespace Umbraco.Controller
{
    public class SearchPageController : RenderMvcController
    {
        private readonly ISearchService _searchService;

        public SearchPageController(ISearchService searchService)
        {
            _searchService = searchService;
        }

        public ActionResult Index(ContentModel model, string query, string page, string category)
        {
            var searchPageModel = new SearchContentModels(model.Content);
            var searchViewModel = new SearchViewModel()
            {
                Query = query,
                Category = category,
                Page = page
            };

            if(!int.TryParse(page, out var pageNumber))
            {
                pageNumber = 1;
            }

            var searchResults = _searchService.GetPageOfContentSearchResults(query, category,
                pageNumber, out var totalItemCount, null);

            searchPageModel.SearchViewModel = searchViewModel;
            searchPageModel.SearchResults = searchResults;

            return CurrentTemplate(searchPageModel);
        }
    }
}