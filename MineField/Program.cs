using Minefield.Enums;
using System;

namespace Minefield
{

    public class Minefield
    {
        public static void Main(string[] args)
        {
            var xLength = 10;
            var yLength = 10;

            var renderer = new FieldRenderer();

            var xStart = new Random().Next(1, xLength - 1);
            var outputWriter = new OutputWriter();

            var field = new Field(xLength, yLength, 3, Difficulty.Easy, renderer, outputWriter,xStart);

            var game = new Game(field, outputWriter);

            game.Start();
        }
    }

    

    

   

    


    



  




   

}
