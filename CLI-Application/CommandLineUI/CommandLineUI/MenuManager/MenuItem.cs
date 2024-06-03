using System;

namespace CommandLineUI.MenuManager
{
    public class MenuItem : MenuElement
    {

        public MenuItem(int id, string text) : base(id, text)
        {
        }

        // Implementation of the Print method in the MenuElement class
        public override void Print(string indent)
        {
            Console.WriteLine("{0}{1}. {2}", indent, Id, Text);
        }
    }
}
