using System.Web.Mvc;
using Umbraco.Web.Mvc;

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