using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.IO;
using System.Linq;
using Microsoft.VisualBasic;
using Newtonsoft.Json;

class Program
{
    static void Main(string[] args)
    {
        string folderPath = @"D:\Chapter 2\Session 3\Part 2\Lession 6";
        string inputString = @"Nathaniel has to think about his theme party this Thursday➚, for his thirty-third ➘birthday. The theme of the party is “Thick and ➘Thin.” For those that don’t know➚, this means➚ that people have to dress in clothing➚ that is either thought of as representing “rich➚” or “➘poor” times. Nathaniel thinks➚ that this phrase is an important thought➚ those that hear➚ it should ➘learn from. The purpose is to be thankful for what you have➚, whether or not things are going well at the ➘time. That way➚, people can both be prepared for the “thin” times➚, as well as the other side of things➚, which is the “➘thick” times. Regardless of who your mother or father is➚, those themes will have an impact on your ➘life.";
        //var fileNames = inputString.Split(',')
        //.Select(f => f.Trim())
        //.ToArray();

        var fileNames = inputString.Split('*')
                .Select(f => f.Trim())
                .ToArray();

        for (int i = 0; i < fileNames.Length; i++)
        {
            string fileName = fileNames[i];
            //string numberedFileName = $"{i + 1}_{fileName}.json";
            string numberedFileName = $"1.json";
            string filePath = Path.Combine(folderPath, numberedFileName);
            var jsonContent = new List<List<WordEntry>>();
            var words = fileName.Split(' ');
            foreach (var word in words)
            {
                string cleanedWord = CleanWord(word);
                if (string.IsNullOrEmpty(cleanedWord))
                    continue;
                var wordEntry = new WordEntry
                {
                    word = cleanedWord,
                    transcription = "",
                    is_evaluation = true,
                    start = null,
                    end = null,
                    phonemas = CreatePhonemes(cleanedWord)
                };
                jsonContent.Add(new List<WordEntry> { wordEntry });
            }
            string jsonOutput = JsonConvert.SerializeObject(jsonContent, Formatting.Indented);
            File.WriteAllText(filePath, jsonOutput);
            Console.WriteLine($"File created: {numberedFileName}");
        } 
    }
    static string CleanWord(string word)
    {
        return word.Replace("➘", ""); // Only remove the "➘" symbol
    }
    static List<Phoneme> CreatePhonemes(string word)
    {
        return word.Select(c => new Phoneme
        {
            @char = c.ToString(),
            transcription = "",
            is_evaluation = false,
            video = "",
            feedback = ""
        }).ToList();
    }
}
public class Phoneme
{
    [JsonProperty(Order = 1)]
    public string @char { get; set; }
    [JsonProperty(Order = 2)]
    public string feedback { get; set; }
    [JsonProperty(Order = 3)]
    public bool is_evaluation { get; set; }

    [JsonProperty(Order = 4)]
    public string transcription { get; set; }

    [JsonProperty(Order = 5)]
    public string video { get; set; }
}

public class WordEntry
{
    [JsonProperty(Order = 1)]
    public object end { get; set; }

    [JsonProperty(Order = 2)]
    public bool is_evaluation { get; set; }
    [JsonProperty(Order = 3)]
    public object start { get; set; }
    [JsonProperty(Order = 4)]
    public string transcription { get; set; }

    [JsonProperty(Order = 5)]
    public string word { get; set; }

    [JsonProperty(Order = 6)]
    public List<Phoneme> phonemas { get; set; }
}