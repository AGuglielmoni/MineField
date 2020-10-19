using Minefield.Enums;
using Minefield.Interfaces;
using System;

namespace Minefield
{
    public class Game
    {
        private IField _field;
        private IOutputWriter _outputWriter;

        public Game(IField field, IOutputWriter outputWriter)
        {
            _field = field;
            _outputWriter = outputWriter;
        }

        public void Start()
        {
            while (_field.Lives > 0 && _field.CurrentPosition.YPosition < _field.YLength - 1)
            {
                var input = _outputWriter.ReadKey();

                switch (input.Key)
                {
                    case ConsoleKey.UpArrow:
                        {
                            _field.Move(MoveOption.Up);
                            break;
                        }
                    case ConsoleKey.DownArrow:
                        {
                            _field.Move(MoveOption.Down);
                            break;
                        }
                    case ConsoleKey.LeftArrow:
                        {
                            _field.Move(MoveOption.Left);
                            break;
                        }
                    case ConsoleKey.RightArrow:
                        {
                            _field.Move(MoveOption.Right);
                            break;
                        }
                    case ConsoleKey.Escape:
                        {
                            return;
                        }
                }
            }

            _outputWriter.WriteLine();
            _outputWriter.WriteLine();
            if (_field.CurrentPosition.YPosition == _field.YLength - 1)
            {
                _outputWriter.WriteLine("CONGRATULATIONS");
            }
            else if (_field.Lives == 0)
            {
                _outputWriter.WriteLine("GAME OVER");
            }

            _outputWriter.WriteLine("Press Enter To Try Again");

            End();
        }

        public void End()
        {
            var input = _outputWriter.ReadKey();

            switch (input.Key)
            {
                case ConsoleKey.Enter:
                    {
                        _field.Setup();
                        Start();
                        break;
                    }
                case ConsoleKey.Escape: { return; }
                default: { End(); break; }
            }

        }
    }
}
