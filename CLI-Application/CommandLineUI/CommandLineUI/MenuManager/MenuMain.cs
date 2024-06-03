using System;
using System.Collections.Generic;

namespace CommandLineUI.MenuManager
{
    // This class implements the Composite design pattern
    public class MenuMain : MenuElement
    {
        private List<MenuElement> children;

        public MenuMain(string text) : base(-1, text)
        {
            children = new List<MenuElement>();
        }

        public void Add(MenuElement menuElement)
        {
            children.Add(menuElement);
        }

        // Implementation of the Print method in the MenuElement class
        public override void Print(string indent)
        {
            Console.WriteLine("\n{0}{1}", indent, Text);
            Console.WriteLine("{0}{1}", indent, "".PadLeft(Text.Length, '-'));

            foreach (MenuElement item in children)
            {
                item.Print(indent + "    ");
            }
        }
    }
}
