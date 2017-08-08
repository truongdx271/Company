using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Company.Areas.Admin.Models
{
    public class MenuModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string MetaTitle { get; set; }
        public string Link { get; set; }
        public int parentId { get; set; }
        public bool? IsSubMenu { get; set; }
        public List<MenuModel> listSubMenu { get; set; }

        public MenuModel()
        {
        }

        public MenuModel(int Id, string Title, string MetaTitle, string Link, int parentId , bool? IsSubMenu)
        {
            this.Id = Id;
            this.Title = Title;
            this.MetaTitle = MetaTitle;
            this.Link = Link;
            this.parentId = parentId;
            this.IsSubMenu = IsSubMenu;
        }
        public MenuModel(int Id, string Title, string MetaTitle, string Link, bool? IsSubMenu, List<MenuModel> listSubMenu)
        {
            this.Id = Id;
            this.Title = Title;
            this.MetaTitle = MetaTitle;
            this.Link = Link;
            this.IsSubMenu = IsSubMenu;
            this.listSubMenu = listSubMenu;
        }
    }
}