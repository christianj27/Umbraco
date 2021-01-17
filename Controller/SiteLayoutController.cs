using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Web.Mvc;
using System.Web.Mvc;
using Umbraco.Models;
using Umbraco.Web;
using System.Runtime.Caching;
using Umbraco.Core.Models.PublishedContent;

namespace Umbraco.Controller
{
    public class SiteLayoutController : SurfaceController
    {
        private const string PARTIAL_VIEW_FOLDER = "~/Views/Partials/SiteLayout/";
        public ActionResult RenderHeadingPage()
        {
            return PartialView(PARTIAL_VIEW_FOLDER + "_HeadingPage.cshtml");
        }
        public ActionResult RenderFooter()
        {
            return PartialView(PARTIAL_VIEW_FOLDER + "_Footer.cshtml");
        }

        /// <summary>
        /// Renders the top navigation in the header partial
        /// </summary>
        /// <returns>Partial view with a model</returns>

        public ActionResult RenderHeader()
        {
            //List<NavigationListItem> nav = GetNavigationModelFromDatabase(); //Make the Navigation Pege Dynamically From Database
            List<NavigationListItem> nav = GetObjectFromCache<List<NavigationListItem>>("mainNav", 5, GetNavigationModelFromDatabase); //Make the Navigation Pege Dynamically From Cache
            return PartialView(PARTIAL_VIEW_FOLDER + "_Header.cshtml", nav);

        }

        /// <summary>
        /// Finds the home page and gets the navigation structure based on it and it's children
        /// </summary>
        /// <returns>A List of NavigationListItems, representing the structure of the site.</returns>
        private List<NavigationListItem> GetNavigationModelFromDatabase()
        {
            //const int HOME_PAGE_POSITION_IN_PATH = 1;
            //int homePageId = int.Parse(CurrentPage.Path.Split(',')[HOME_PAGE_POSITION_IN_PATH]);
            //IPublishedContent homePage = Umbraco.Content(homePageId);

            IPublishedContent homePage = CurrentPage.AncestorOrSelf(1).DescendantsOrSelf().Where(x => x.ContentType.Alias == "homepage").FirstOrDefault();
            List<NavigationListItem> nav = new List<NavigationListItem>();
            nav.Add(new NavigationListItem(new NavigationLink(homePage.Url, homePage.Name)));
            nav.AddRange(GetChildNavigationList(homePage));
            return nav;
        }
        private List<NavigationListItem> GetChildNavigationList(IPublishedContent page)
        {
            List<NavigationListItem> listItems = null;
            var childPages = page.Children.Where(x => x.IsVisible()).Where(x => !x.HasValue("excludeFromTopNavigation") || (x.HasValue("excludeFromTopNavigation") && !x.Value<bool>("excludeFromTopNavigation")));
            if (childPages != null && childPages.Any() && childPages.Count() > 0)
            {
                listItems = new List<NavigationListItem>();
                foreach (var childPage in childPages)
                {
                    NavigationListItem listItem = new NavigationListItem(new NavigationLink(childPage.Url, childPage.Name));
                    listItem.Items = GetChildNavigationList(childPage);
                    listItems.Add(listItem);
                }
            }
            return listItems;
        }

        //Use Caching

        /// <summary>
        /// A generic function for getting and setting objects to the memory cache.
        /// </summary>
        /// <typeparam name="T">The type of the object to be returned.</typeparam>
        /// <param name="cacheItemName">The name to be used when storing this object in the cache.</param>
        /// <param name="cacheTimeInMinutes">How long to cache this object for.</param>
        /// <param name="objectSettingFunction">A parameterless function to call if the object isn't in the cache and you need to set it.</param>
        /// <returns>An object of the type you asked for</returns>
        private static T GetObjectFromCache<T>(string cacheItemName, int cacheTimeInMinutes, Func<T> objectSettingFunction)
        {
            ObjectCache cache = MemoryCache.Default;
            var cachedObject = (T)cache[cacheItemName];
            if (cachedObject == null)
            {
                CacheItemPolicy policy = new CacheItemPolicy();
                policy.AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(cacheTimeInMinutes);
                cachedObject = objectSettingFunction();
                cache.Set(cacheItemName, cachedObject, policy);
            }
            return cachedObject;
        }
    }
}

