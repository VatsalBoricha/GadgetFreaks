using Microsoft.AspNetCore.Mvc;
using GadgetFreaks.Models;

namespace GadgetFreaks.Components.ViewComponents
{
    public class NavigationMenuViewComponent: ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var menuItems = new List<MenuItem> {
                new MenuItem{Controller = "Home" , Action= "Index" , Label="Home"},
                new MenuItem{Controller = "Shop", Action = "Index", Label = "Shop"},

                new MenuItem{Controller = "Categories" , Action= "Index" , Label="Category" ,DropDownItems =new List<MenuItem>{
                new MenuItem{Controller = "Categories" , Action= "Index" , Label="List"},
                new MenuItem{Controller = "Categories" , Action= "Create" , Label="Create"},
                }, Authorized = true , AllowedRoles = new List<string> {"Administrator"} },
                new MenuItem{Controller = "Gadgets" , Action= "Index" , Label="Gadget" ,DropDownItems =new List<MenuItem>{
                new MenuItem{Controller = "Gadgets" , Action= "Index" , Label="List"},
                new MenuItem{Controller = "Gadgets" , Action= "Create" , Label="Create"},
                }, Authorized = true },
                new MenuItem{Controller = "Home" , Action= "Privacy" , Label="Privacy"},
            };
            return View(menuItems);

        }
    }
}
