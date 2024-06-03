namespace CommandLineUI.MenuManager
{
    public abstract class MenuElement
    {
        public int Id { get; }
        public string Text { get; }

        public MenuElement(int id, string text)
        {
            Id = id;
            Text = text;
        }

        public abstract void Print(string indent);
    }
}
