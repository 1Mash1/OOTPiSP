namespace MyShape
{
    public interface IPlugin
    {
        string Name { get; } // AddPluginButton: plugin.Name
        ShapeFactory GetFactory(); // LoadPlugins: plugin.GetFactory()
        IDrawStrategy GetStrategy(); // LoadPlugins: plugin.GetStrategy()
    }
}