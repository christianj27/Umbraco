using System.Collections.Generic;
using System.Linq;

namespace Umbraco.Models
{
    public class CategoryListItem
    {
        public string Text { get; set; }
        public CategoryLink Link { get; set; }
        public List<CategoryListItem> Items { get; set; }
        public bool HasChildren { get { return Items != null && Items.Any() && Items.Count > 0; } }

        public CategoryListItem()
        { }

        public CategoryListItem(CategoryLink link)
        {
            Link = link;
        }

        public CategoryListItem(string text)
        {
            Text = text;
        }
    }
}

