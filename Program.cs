using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

            string markdown = File.ReadAllText(inputFile);
            string html = ConvertMarkdownToHtml(markdown);

            if (outputFile != null)
            {
                File.WriteAllText(outputFile, html);
                Console.WriteLine("HTML successfully written to " + outputFile);
            }
            else
            {
                Console.WriteLine(html);
            }

        }
        static string ConvertMarkdownToHtml(string markdown)
        {
            markdown = Regex.Replace(markdown, @"\*\*(.*?)\*\*", "<b>$1</b>");

            markdown = Regex.Replace(markdown, @"_(.*?)_", "<i>$1</i>");

            markdown = Regex.Replace(markdown, @"```([\s\S]*?)```", "<pre>$1</pre>");

            markdown = Regex.Replace(markdown, @"`(.*?)`", "<tt>$1</tt>");

            markdown = Regex.Replace(markdown, @"(?:\r?\n){2,}", "</p><p>");

            markdown = "<p>" + markdown + "</p>";

            return markdown;
        }
    }
}
