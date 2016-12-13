namespace gEngine.Graph.Interface
{
    public interface IRect : IObject
    {
        double Left { set; get; }
        double Top { set; get; }
        double Width { set; get; }
        double Height { set; get; }
    }
}
