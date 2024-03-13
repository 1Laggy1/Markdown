using Microsoft.SqlServer.Server;
using System;
using System.IO;
using System.Text.RegularExpressions;

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
            string format = null;

            for (int i = 1; i < args.Length; i++)
            {
                if (args[i] == "--out" || args[i] == "-o")
                {
                    if (i + 1 < args.Length)
                    {
                        outputFile = args[i + 1];
                        i++;
                    }
                }
                else if (args[i].StartsWith("--format="))
                {
                    format = args[i].Substring("--format=".Length);
                }
            }
            string markdown = File.ReadAllText(inputFile);
            string html = "";
            if ((format == "ansi" || format == null || format == "") && outputFile == null)
            {
                html = ConvertMarkdownToHtmlAnsi(markdown);
            }
            else
            {
                html = ConvertMarkdownToHtml(markdown);
            }

            if (outputFile != null)
            {
                File.WriteAllText(outputFile, html);
                Console.WriteLine("HTML successfully written to " + outputFile);
            }
            else
            {
                Console.WriteLine(html);
            }
            //test1
        }
        public static string ConvertMarkdownToHtml(string markdown)
        {
            markdown = Regex.Replace(markdown, @"\*\*(.*?)\*\*", "<b>$1</b>");

            markdown = Regex.Replace(markdown, @"_(.*?)_", "<i>$1</i>");

            markdown = Regex.Replace(markdown, @"```([\s\S]*?)```", "<pre>$1</pre>");

            markdown = Regex.Replace(markdown, @"`(.*?)`", "<tt>$1</tt>");

            markdown = Regex.Replace(markdown, @"(?:\A|\r?\n\r?\n)(.*?)(?=\r?\n\r?\n|\z)", "\n<p>$1</p>", RegexOptions.Singleline);

            string[] patterns = { "<b><tt><i>",
"<b><i><tt>",
"<tt><b><i>",
"<tt><i><b>",
"<i><b><tt>",
"<i><tt><b>",
"<b><tt>",
"<b><i>",
"<tt><b>",
"<tt><i>",
"<i><b>",
"<i><tt>" };
            foreach (string pattern in patterns)
            {
                if (Regex.IsMatch(markdown, pattern))
                {
                    throw new Exception("Wrong format: " + pattern);
                }
            }


            return markdown;
        }
        public static string ConvertMarkdownToHtmlAnsi(string markdown)
        {
            markdown = Regex.Replace(markdown, @"\*\*(.*?)\*\*", "\x1b[1m$1\x1b[0m");

            markdown = Regex.Replace(markdown, @"_(.*?)_", "\x1b[3m$1\x1b[0m");

            markdown = Regex.Replace(markdown, @"```([\s\S]*?)```", "\x1b[7m$1\x1b[0m");

            markdown = Regex.Replace(markdown, @"`(.*?)`", "\x1b[2m$1\x1b[0m");

            return markdown;
        }

    }
}
