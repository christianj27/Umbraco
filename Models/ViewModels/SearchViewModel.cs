using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Umbraco.Models.ViewModels
{
    public class SearchViewModel
    {
        [Display(Name = "Search Term")]
        [Required(ErrorMessage ="You must Enter a search term")]
        public string Query { get; set; }

        [Display(Name = "Category")]
        public string Category { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }

        [Display(Name = "Page Number")]
        public string Page { get; set; }
    }
}