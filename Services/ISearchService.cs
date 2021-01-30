using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Examine;
using Umbraco.Core.Models.PublishedContent;

namespace Umbraco.Services
{
    public interface ISearchService
    {
        IEnumerable<ISearchResult> GetPageOfSearchResults(string searchTerm,
            string category, int pageNumber, out long totalItemCount, string[] docTypeAliases, string searchType,
            int pageSize = 10);

        IEnumerable<IPublishedContent> GetPageOfContentSearchResults(string searchTerm,
            string category, int pageNumber, out long totalItemCount, string[] docTypeAliases,
            int pageSize = 10);
    }
}