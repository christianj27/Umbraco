using Umbraco.Web.Models;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Models.ViewModels;
using System.Collections.Generic;

namespace Umbraco.Models
{
    public class SearchContentModels : ContentModel
    {
        public SearchContentModels(IPublishedContent content) : base(content)
        {

        }
        public SearchViewModel SearchViewModel { get; set; }

        public IEnumerable<IPublishedContent> SearchResults { get; set; }
    }
}