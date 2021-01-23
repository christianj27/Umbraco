using System.Web.Mvc;
using Umbraco.Web.Mvc;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Models;
using Umbraco.Web;
using Umbraco.Core.Models.PublishedContent;


namespace Umbraco.Controller
{
    public class HomePageController : SurfaceController
    {
        private const string PARTIAL_VIEW_FOLDER = "~/Views/Partials/HomePage/";

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
            return PartialView(PARTIAL_VIEW_FOLDER + "_SideBar.cshtml");
        }
    }
}