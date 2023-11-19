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
        var scriptures = new List<Scripture>
        {
            new Scripture(new Reference("John 3:16"), "For God so loved the world, that he gave his only Son, that whoever believes in him should not perish but have eternal life."),
            new Scripture(new Reference("Proverbs 3:5-6"), "Trust in the Lord with all your heart, and do not lean on your own understanding. In all your ways acknowledge him, and he will make straight your paths."),
            new Scripture(new Reference("Psalm 23:1"), "The Lord is my shepherd; I shall not want."),
        };

        Console.WriteLine("Choose a Scripture:");
        for (int i = 0; i < scriptures.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {scriptures[i].Reference.ReferenceText}");
        }
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

        Scripture selectedScripture;
        if (selectedOption <= scriptures.Count)
        {
            selectedScripture = scriptures[selectedOption - 1];
        }
        else
        {
            Console.Write("Enter the reference for your verse (e.g., John 1:1): ");
            var referenceText = Console.ReadLine();
            Console.Write("Enter the text for your verse: ");
            var verseText = Console.ReadLine();
            var userReference = new Reference(referenceText);
            selectedScripture = new Scripture(userReference, verseText);
            scriptures.Add(selectedScripture);
        }

        while (selectedScripture.Words.Any(word => !word.Hidden))
        {
            selectedScripture.Display();
            Console.Write("Press Enter to continue or type 'quit' to exit: ");
            var userInput = Console.ReadLine();

            if (userInput?.ToLower() == "quit")
            {
                break;
            }

            selectedScripture.HideRandomWords(new Random().Next(1, 4));
        }

        Console.WriteLine("Program ended.");
    }
}