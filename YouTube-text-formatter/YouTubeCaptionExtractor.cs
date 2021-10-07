using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace YouTube_text_formatter
{
    class YouTubeCaptionExtractor
    {
        static string allWords = "";
        static List<string> FineLines = new List<string>();
        static int currentLineLimit = 20;
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
            AppendLines(OnlyChatachters);
            WriteFileDown(FineLines.ToArray());
            System.Diagnostics.Debug.WriteLine("Done");
        }
        static private bool IsTime(string line)
        {
            if (line.Contains(':') && line[0].ToString().Any(c => char.IsDigit(c)) && line[1].ToString().Any(c => char.IsDigit(c)))
            {
                return true;
            }
            return false;
        }
        static private bool IsSoundEffect(string line)
        {
            if (line.Contains('[') && line.Contains(']'))
            {
                return true;
            }
            return false;
        }
        static private void AppendLines(List<string> lines)
        {
            //int First = 0; int Second = 1; int Third = 2;
            var words = allWords.Split(' ');
            linesCopy = new List<string>(words.ToList());
            int div = linesCopy.Count / 20; //quotient is 1
            int mod = linesCopy.Count % 20; //remainder is 2
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
        static void AddWordsToLine(int count,bool Islast) {
            string tempString = "";
            for (int x = 0; x < count; x++) {
                tempString += linesCopy[x] + " ";
            }
            FineLines.Add(tempString);
            if(!Islast) RemoveWordsFromList(count);
        }
        static void RemoveWordsFromList(int count)
        {
            linesCopy.RemoveRange(0, count);

        }
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
            static private bool WriteFileDown(string[] lines)
        {
            string path = @"c:\Youtube\youOut.txt";

            // This text is added only once to the file.
            if (!File.Exists(path))
            {
                // Create a file to write to.
                File.WriteAllLines(path, lines, Encoding.UTF8);
                return true;
            }
            else { return false; }
        }
    }
}
