namespace Lab2_PontusEkdahl;

public class Option
{
    public string Name { get; }
    public Menu.MenuItems MenuItem { get; }

    public Option(string name, Menu.MenuItems menuItem)
    {
        Name = name;
        MenuItem = menuItem;
    }
}