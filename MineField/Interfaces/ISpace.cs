namespace Minefield.Interfaces
{
    public interface ISpace
    {

        int XPosition { get; set; }
        int YPosition { get; set; }
        bool Mine { get; set; }
        bool Route { get; set; }
        bool Visited { get; set; }
    }
}
