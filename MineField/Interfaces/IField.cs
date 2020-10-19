using Minefield.Enums;

namespace Minefield.Interfaces
{
    public interface IField
    {
        Difficulty CurrentDifficulty { get; }

        ISpace CurrentPosition { get; }

        int Lives { get; }

        int XLength { get; }

        int YLength { get; }

        void Move(MoveOption direction);

        void Setup();

        int Moves { get; set; }

        ISpace[,] Spaces { get; }
    }
}
