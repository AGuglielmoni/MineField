using Minefield.Interfaces;
using System;

namespace Minefield.Enums
{
    public class Field : IField
    {
        public ISpace[,] Spaces { get; private set; }
        public ISpace CurrentPosition { get; private set; }
        public int Lives { get; private set; }
        public int XLength { get; private set; }
        public int YLength { get; private set; }
        public Difficulty CurrentDifficulty { get; private set; }
        public int Moves { get; set; }

        private int _xStart;
        private int _yStart;
        private int _initialLives;
        private IFieldRenderer _renderer;
        private IOutputWriter _outputWriter;

        public Field(int xLength, int yLength, int lives, Difficulty difficulty, IFieldRenderer renderer, IOutputWriter outputWriter, int xStart, int yStart = 0)
        {
            XLength = xLength;
            YLength = yLength;
            CurrentDifficulty = difficulty;
            _initialLives = lives;
            _renderer = renderer;
            _xStart = xStart;
            _yStart = yStart;
            _outputWriter = outputWriter;

            Setup();
        }

        public void Setup()
        {
            Lives = _initialLives;
            Spaces = new Space[XLength, YLength];
            Moves = 0;
            CreateSpaces();
            CurrentPosition = Spaces[_xStart, _yStart];
            CurrentPosition.Visited = true;

            RouteHelper routeHelper = new RouteHelper(Spaces, CurrentPosition, XLength);
            routeHelper.PlotRoute();

            GenerateMines();

            _renderer.Render(this,_outputWriter);
        }

        public void Move(MoveOption direction)
        {
            switch (direction)
            {
                case MoveOption.Up:
                    if (CurrentPosition.YPosition < YLength - 1)
                    {
                        CurrentPosition = Spaces[CurrentPosition.XPosition, CurrentPosition.YPosition + 1];
                        CheckMine();
                    }
                    break;
                case MoveOption.Down:
                    if (CurrentPosition.YPosition > 0)
                    {
                        CurrentPosition = Spaces[CurrentPosition.XPosition, CurrentPosition.YPosition - 1];
                        CheckMine();
                    }
                    break;
                case MoveOption.Left:
                    if (CurrentPosition.XPosition > 0)
                    {
                        CurrentPosition = Spaces[CurrentPosition.XPosition - 1, CurrentPosition.YPosition];
                        CheckMine();
                    }
                    break;
                case MoveOption.Right:
                    if (CurrentPosition.XPosition < XLength - 1)
                    {
                        CurrentPosition = Spaces[CurrentPosition.XPosition + 1, CurrentPosition.YPosition];
                        CheckMine();
                    }
                    break;
            }
            _renderer.Render(this,_outputWriter);
        }

        private void CheckMine()
        {
            if (CurrentPosition.Mine)
            {
                Lives -= 1;
            }
            CurrentPosition.Visited = true;
            Moves += 1;
        }

        private void CreateSpaces()
        {
            for (var x = 0; x < XLength; x++)
            {
                for (var y = 0; y < YLength; y++)
                {
                    Spaces[x, y] = new Space(x, y);
                }
            }
        }

        //place mines randomly depending on the difficulty, avoiding spaces allocated for a viable route
        private void GenerateMines()
        {
            for (var x = 0; x < XLength; x++)
            {
                for (var y = 0; y < YLength; y++)
                {
                    Spaces[x, y].Mine = Spaces[x, y].Route == false && (new Random().Next(1, 101) < (int)CurrentDifficulty);
                }
            }
        }
    }
}
