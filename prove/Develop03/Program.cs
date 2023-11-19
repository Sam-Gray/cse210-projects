using System;
using System.Collections.Generic;
using System.Linq;

class Word
{
    public string Text { get; }
    public bool Hidden { get; set; }

    public Word(string text, bool hidden = false)
    {
        Text = text;
        Hidden = hidden;
    }
}

class Reference
{
    public string ReferenceText { get; }

    public Reference(string referenceText)
    {
        ReferenceText = referenceText;
    }
}

class Scripture
{
    private readonly Reference reference;
    private readonly List<Word> words;

    public List<Word> Words => words;

    public Scripture(Reference reference, string text)
    {
        this.reference = reference;
        words = text.Split().Select(word => new Word(word)).ToList();
    }

    public void HideRandomWords(int count)
    {
        var wordsToHide = words.Where(word => !word.Hidden).OrderBy(_ => Guid.NewGuid()).Take(count);

        foreach (var word in wordsToHide)
        {
            word.Hidden = true;
        }
    }

    public void Display()
    {
        Console.Clear();
        Console.WriteLine($"{reference.ReferenceText}: {string.Join(" ", words.Select(word => word.Hidden ? "___" : word.Text))}");
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("Choose a Scripture:");
        var scriptures = new List<Scripture>
        {
            new Scripture(new Reference("1 Nephi 3:7"), "“And it came to pass that I, Nephi, said unto my father: I will go and do the things which the Lord hath commanded, for I know that the Lord giveth no commandments unto the children of men, save he shall prepare a way for them that they may accomplish the thing which he commandeth them.”

"),
            new Scripture(new Reference("Proverbs 3:5-6"), "Trust in the Lord with all thine heart; and lean not unto thine own understanding. In all thy ways acknowledge him, and he shall direct thy paths."),
            new Scripture(new Reference("Mosiah 2:17"), "when ye are in the service of your fellow beings ye are only in the service of your God."),
        };

        Console.WriteLine($"{scriptures.Count + 1}. Add your own verse");

        int selectedOption;
        while (true)
        {
            Console.Write("Enter the number of your choice: ");
            if (int.TryParse(Console.ReadLine(), out selectedOption) && selectedOption >= 1 && selectedOption <= scriptures.Count + 1)
            {
                break;
            }
            Console.WriteLine("Invalid choice. Please enter a valid number.");
        }

        int wordsToHideCount = 2; // Default number of words to hide
        if (selectedOption <= scriptures.Count)
        {
            Console.Write($"Enter the number of words to disappear each time (default is {wordsToHideCount}): ");
            int.TryParse(Console.ReadLine(), out wordsToHideCount);
        }
        else
        {
            Console.Write("Enter the reference for your verse (e.g., John 1:1): ");
            var referenceText = Console.ReadLine();
            Console.Write("Enter the text for your verse: ");
            var verseText = Console.ReadLine();
            var userReference = new Reference(referenceText);
            var userScripture = new Scripture(userReference, verseText);
            scriptures.Add(userScripture);
        }

        // Exceeding requirements:
        // - Added an option for the user to pick the number of words that disappear each time.
        while (selectedScripture.Words.Any(word => !word.Hidden))
        {
            selectedScripture.Display();
            Console.Write("Press Enter to continue or type 'quit' to exit: ");
            var userInput = Console.ReadLine();

            if (userInput?.ToLower() == "quit")
            {
                break;
            }

            selectedScripture.HideRandomWords(wordsToHideCount);
        }

        Console.WriteLine("Program ended.");
    }
}