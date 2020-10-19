using Minefield.Interfaces;
using System;

namespace Minefield
{
    public class FieldRenderer : IFieldRenderer
    {
        public void Render(IField field, IOutputWriter outputWriter)
        {
            outputWriter.Clear();
            outputWriter.WriteLine("Reach the top of the minefield");

            for (var y = field.YLength - 1; y >= 0; y--)
            {

                for (var x = 0; x < field.XLength; x++)
                {
                    //set a different bracket style for better indication player has landed on a mine space
                    if (field.Spaces[x, y].Mine && field.Spaces[x, y].Visited)
                    {
                        outputWriter.Write("{");
                    }
                    else
                    {
                        outputWriter.Write("[");
                    }

                    if (field.Spaces[x, y].XPosition == field.CurrentPosition.XPosition && field.Spaces[x, y].YPosition == field.CurrentPosition.YPosition)
                    {
                        outputWriter.Write("P");
                    }
                    else if (field.Spaces[x, y].Mine && field.Spaces[x, y].Visited)
                    {
                        outputWriter.Write("X");
                    }
                    else
                    {
                        outputWriter.Write(" ");
                    }

                    if (field.Spaces[x, y].Mine && field.Spaces[x, y].Visited)
                    {
                        outputWriter.Write("}");
                    }
                    else
                    {
                        outputWriter.Write("]");
                    }

                }
                outputWriter.Write(" ");
                outputWriter.Write((y + 1).ToString().ToUpper());
                outputWriter.Write(" ");
                outputWriter.WriteLine();
            }

            outputWriter.Write(" ");

            for (var x = 0; x < field.XLength; x++)
            {
                outputWriter.Write(((char)(x + 65)).ToString());
                outputWriter.Write("  ");
            }

            outputWriter.WriteLine();
            outputWriter.Write("Current Lives: " + field.Lives.ToString());
            outputWriter.WriteLine();
            outputWriter.Write("Current Moves: " + field.Moves.ToString());
            outputWriter.WriteLine();
        }
    }
}
