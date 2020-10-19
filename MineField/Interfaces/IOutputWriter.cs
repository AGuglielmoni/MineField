using System;

namespace Minefield.Interfaces
{
    public interface IOutputWriter
    {
        void Write(string text);

         void WriteLine(string text = "");

        ConsoleKeyInfo ReadKey();

        void Clear();
    }
}
