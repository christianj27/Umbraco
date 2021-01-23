using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web.Mvc;
using Umbraco.Models;
using Umbraco.Web;
using Umbraco.Core.Models.PublishedContent;

namespace Umbraco.Controller
{
    public class BlogController : SurfaceController
    {
        private const string PARTIAL_VIEW_FOLDER = "~/Views/Partials/Blog/";

        public ActionResult RenderPostList(int numberOfItems)
        {
            List<BlogPreview> model = new List<BlogPreview>();

            IPublishedContent blogPage = CurrentPage.AncestorOrSelf().DescendantsOrSelf().Where(x => x.ContentType.Alias == "blogEntries").FirstOrDefault(); //Remove number parameter in AncestorOrSelf() so it can search throug all in the navigation menu

            var pageNumber = 1;

            var pageSize = numberOfItems;

            var pageOfArticles = blogPage.Children.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            var totalItemCount = blogPage.Children.Count();

            var pageCount = totalItemCount > 0 ? Math.Ceiling((double)totalItemCount / pageSize) : 1;
            
            
            foreach (IPublishedContent page in blogPage.Children.OrderByDescending(x => x.UpdateDate).Take(numberOfItems))
            {
                var pageName = page.Name;
                var intro = page.Value<string>("ArticleIntro");
                var imageUrl = page.Value<IPublishedContent>("ArticleImage").Url;
                var pageUrl = page.Url;
                var creator = page.CreatorName;
                var publishedDate = page.CreateDate.ToString("MMMM dd, yyyy");

                model.Add(new BlogPreview(pageName, intro, imageUrl, pageUrl, creator, publishedDate));
            }

            return PartialView(PARTIAL_VIEW_FOLDER + "_PostList.cshtml", model);
        }

    }
}

//@{
//    var pageSize = 8;
//    if (Model.Content.HasValue("numberOfItemsPerPage"))
//    {
//        pageSize = Model.Content.GetPropertyValue<int>("numberOfItemsPerPage");
//    }

//    var page = 1; int.TryParse(Request.QueryString["page"], out page);
//    var items = Umbraco.TypedContent(Model.Content.Id).Children.Where(x => x.DocumentTypeAlias == "exampleAlias" && x.IsVisible());
//    var totalPages = (int)Math.Ceiling((double)items.Count() / (double)pageSize);

//    if (page > totalPages)
//    {
//        page = totalPages;
//    }
//    else if (page < 1)
//    {
//        page = 1;
//    }

//    foreach (var item in items.Skip((page - 1) * pageSize).Take(pageSize).OrderBy("createDate descending"))
//    {

//     < div class= "example-div" >
 
//             < h2 > @item.GetPropertyValue("example") </ h2 >
 
//     </ div >
//}

//if (totalPages > 1)
//{
//    < div class= "pagination" >
 
//         < ul >
//             @if(page > 1)
//            {
//                < li >< a href = "?page=@(page-1)" > Prev </ a ></ li >
//            }
//@for(int p = 1; p < totalPages + 1; p++)
//            {
//                < li class= "@(p == page ? "active " : string.Empty)" >
   
//                       < a href = "?page=@p" > @p </ a >
    
//                    </ li >
//            }
//            @if(page < totalPages)
//            {
//                < li >< a href = "?page=@(page+1)" > Next </ a ></ li >
//            }
//        </ ul >
//    </ div >
//}
//}