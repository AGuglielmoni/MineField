using Minefield.Interfaces;

namespace Minefield
{
    public class Space : ISpace
    {
        public Space(int xPostion, int yPosition)
        {
            XPosition = xPostion;
            YPosition = yPosition;
        }

        public int XPosition { get; set; }
        public int YPosition { get; set; }
        public bool Mine { get; set; }
        public bool Route { get; set; }
        public bool Visited { get; set; }
    }
}
