using Minefield.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Minefield
{
    public class OutputWriter : IOutputWriter
    {
        public void Clear()
        {
            Console.Clear();
        }

        public ConsoleKeyInfo ReadKey()
        {
            return Console.ReadKey();
        }

        public void Write(string text)
        {
            Console.Write(text);
        }

        public void WriteLine(string text = "")
        {
            Console.WriteLine(text);
        }
    }
}
