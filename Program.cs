using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Markdown
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("Usage: <inputFile> [--out <outputFile>]");
                return;
            }

            string inputFile = args[0];
            string outputFile = null;

            if (args.Length >= 3 && args[1] == "--out")
            {
                outputFile = args[2];
            }

            if (outputFile != null)
            {

            }
            else
            {

            }

        }
    }
}
