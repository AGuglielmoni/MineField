using Minefield.Enums;
using Minefield.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Minefield
{
    public class RouteHelper
    {
        private ISpace[,] _spaces;
        private ISpace _currentPosition;
        private int _sideLength;

        public RouteHelper(ISpace[,] spaces, ISpace currentPosition, int sideLength)
        {
            _spaces = spaces;
            _currentPosition = currentPosition;
            _sideLength = sideLength;
        }

        //check if the player is on the edge of the minefield
        private IEnumerable<int> GetViableMoves(ISpace position)
        {
            return Enumerable.Range(0, 4).Where(i => (position.YPosition > 0 || i != (int)MoveOption.Down)
            && (position.XPosition > 0 || i != (int)MoveOption.Left)
            && (position.XPosition < _sideLength - 1 || i != (int)MoveOption.Right));
        }

        //method to make sure that there is a viable way to reach the end of the minefield
        public void PlotRoute()
        {
            ISpace currentRoute = _currentPosition;

            while (currentRoute.YPosition < _sideLength - 1)
            {
                currentRoute.Route = true;

                var viableMoves = GetViableMoves(currentRoute).ToList();

                var nextMove = (MoveOption)viableMoves[(new Random().Next(0, viableMoves.Count()))];

                //find the next space that ideally hasnt been used yet
                bool foundNext = false;
                ISpace nextViable = new Space(0, 0);
                while (foundNext == false && viableMoves.Count() > 1)
                {
                    switch (nextMove)
                    {
                        case MoveOption.Down:
                            nextViable = _spaces[currentRoute.XPosition, currentRoute.YPosition - 1];
                            break;
                        case MoveOption.Up:
                            nextViable = _spaces[currentRoute.XPosition, currentRoute.YPosition + 1];
                            break;
                        case MoveOption.Left:
                            nextViable = _spaces[currentRoute.XPosition - 1, currentRoute.YPosition];
                            break;
                        case MoveOption.Right:
                            nextViable = _spaces[currentRoute.XPosition + 1, currentRoute.YPosition];
                            break;
                    }

                    if (!nextViable.Route)
                    {
                        foundNext = true;
                    }
                    else
                    {
                        viableMoves = viableMoves.Where(m => m != (int)nextMove).ToList();
                        nextMove = (MoveOption)viableMoves[(new Random().Next(0, viableMoves.Count()))];
                    }
                }
                currentRoute = nextViable;
            }
            currentRoute.Route = true;

        }

    }
}
