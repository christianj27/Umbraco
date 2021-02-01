using System.Web.Mvc;
using Umbraco.Web.Mvc;
using Umbraco.Services;
using System.Collections.Generic;
using Umbraco.Models;
using Umbraco.Models.ViewModels;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;
using System.Linq;

namespace Umbraco.Controller
{
    public class HomePageController : SurfaceController
    {
        private const string PARTIAL_VIEW_FOLDER = "~/Views/Partials/HomePage/";
        private readonly IDataTypeValueService _dataTypeValueService;

        public HomePageController(IDataTypeValueService dataTypecValueService)
        {
            _dataTypeValueService = dataTypecValueService;
        }

        public ActionResult RenderMainBanner()
        {
            return PartialView(PARTIAL_VIEW_FOLDER + "_MainBanner.cshtml");
        }
        public ActionResult RenderBlogPost()
        {
            return PartialView(PARTIAL_VIEW_FOLDER + "_BlogPost.cshtml");
        }
        public ActionResult RenderSideBar()
        {
            IEnumerable<SelectListItem> categories =
                _dataTypeValueService.GetItemsFromValueListDataType("[MULTICHECKBOXLIST] Category List", null);

            List<CategoryListItem> nav = GetCategoryModelFromDatabase(); //Make the Navigation Pege Dynamically From Database
            return PartialView(PARTIAL_VIEW_FOLDER + "_SideBar.cshtml", categories);
        }

        private List<CategoryListItem> GetCategoryModelFromDatabase()
        {
            IPublishedContent homePage = CurrentPage.AncestorOrSelf(1).DescendantsOrSelf().Where(x => x.ContentType.Alias == "homepage").FirstOrDefault();
            List<CategoryListItem> nav = new List<CategoryListItem>();
            nav.Add(new CategoryListItem(new CategoryLink(homePage.Url, homePage.Name)));
            nav.AddRange(GetChildCategoryList(homePage));
            return nav;
        }
        private List<CategoryListItem> GetChildCategoryList(IPublishedContent page)
        {
            List<CategoryListItem> listItems = null;
            var childPages = page.Children.Where(x => x.IsVisible()).Where(x => x.Level <= 2).Where(x => !x.HasValue("excludeFromTopCategory") || (x.HasValue("excludeFromTopCategory") && !x.Value<bool>("excludeFromTopCategory")));
            if (childPages != null && childPages.Any() && childPages.Count() > 0)
            {
                listItems = new List<CategoryListItem>();
                foreach (var childPage in childPages)
                {
                    CategoryListItem listItem = new CategoryListItem(new CategoryLink(childPage.Url, childPage.Name));
                    listItem.Items = GetChildCategoryList(childPage);
                    listItems.Add(listItem);
                }
            }
            return listItems;
        }
    }
}