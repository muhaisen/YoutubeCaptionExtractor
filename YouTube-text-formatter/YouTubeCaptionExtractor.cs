using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace YouTube_text_formatter
{
    /// <summary>
    /// Extracts Youtube Caption
    /// </summary>
    class YouTubeCaptionExtractor
    {
        static string allWords = "";
        static readonly int currentLineLimit = 20;
        static List<string> FineLines = new List<string>();
        static List<string> linesCopy = new List<string>();

        static void Main(string[] args)
        {
            string filePath = @"c:\Youtube\you2.txt";
            string[] lines = System.IO.File.ReadAllLines(filePath);
            List<string> OnlyChatachters = new List<string>();

            for (int x = 0; x < lines.Length; x++)
            {
                if (!IsTime(lines[x].ToString()))
                {
                    OnlyChatachters.Add(lines[x]);
                }
            }
            GetLongString(OnlyChatachters.ToArray());
            AppendLines();
            WriteFileDown(FineLines.ToArray());
            System.Diagnostics.Debug.WriteLine("Done");
        }
        /// <summary>
        /// Checks if the passed line contains time only (in format of 10:52)
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
            static private bool IsTime(string line)
        {
            if (line.Contains(':') && line[0].ToString().Any(c => char.IsDigit(c)) && line[1].ToString().Any(c => char.IsDigit(c)))
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// Check if the line is only sound efffect such as applause or laughs.
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        static private bool IsSoundEffect(string line)
        {
            if (line.Contains('[') && line.Contains(']'))
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// Creates lines from the one line of all the text. 
        /// </summary>
        /// <param name="lines"></param>
        static private void AppendLines()
        {
            // The inital definition of the line length is 20 words.
            var words = allWords.Split(' ');
            linesCopy = new List<string>(words.ToList());
            int div = linesCopy.Count / 20;
            int mod = linesCopy.Count % 20;
            for (int x = 0; x < div+1; x++) {
                // If the current iteration is not the last one.
                if (div != x) {
                    AddWordsToLine(currentLineLimit,false);
                }
                else {
                    AddWordsToLine(mod, true);
                }
            }
        }
        /// <summary>
        /// Adds single lines of the defined lenght to the list of strings prapring for the last phase.
        /// </summary>
        /// <param name="count"></param>
        /// <param name="Islast"></param>
        static void AddWordsToLine(int count,bool Islast) {
            string tempString = "";
            for (int x = 0; x < count; x++) {
                tempString += linesCopy[x] + " ";
            }
            FineLines.Add(tempString);
            if(!Islast) RemoveWordsFromList(count);
        }
        /// <summary>
        /// Pops the X count number of the list.
        /// </summary>
        /// <param name="count"></param>
        static void RemoveWordsFromList(int count)
        {
            linesCopy.RemoveRange(0, count);

        }
        /// <summary>
        /// Iterates over all the lines, aggregates them into one long string.
        /// </summary>
        /// <param name="lines"></param>
        static void GetLongString(string[] lines)
        {
            for (int x = 0; x < lines.Length; x++)
            {
                if (!IsSoundEffect(lines[x]))
                {
                    allWords += lines[x] + " ";
                }
                 
            }
        }
        /// <summary>
        /// Writes a file down to a directory of your liking. 
        /// </summary>
        /// <param name="lines"></param>    
        static void WriteFileDown(string[] lines)
        {
            string path = @"c:\Youtube\youOut.txt";

            // This text is added only once to the file.
            if (!File.Exists(path))
            {
                // Create a file to write to.
                File.WriteAllLines(path, lines, Encoding.UTF8);
            }
            else { System.Diagnostics.Debug.WriteLine("Error saving the file to the given directory"); }
        }
    }
}
