using System;
using System.Collections.Generic;
using System.IO;

class Entry
{
    public string Prompt { get; set; }
    public string Response { get; set; }
    public string Date { get; set; }
    public string Location { get; set; }
    public string Mood { get; set; }
    public int Rating { get; set; }

    public Entry(string prompt, string response, string date, string location, string mood, int rating)
    {
        Prompt = prompt;
        Response = response;
        Date = date;
        Location = location;
        Mood = mood;
        Rating = rating;
    }

    public override string ToString()
    {
        return $"{Date}: {Prompt}\nResponse: {Response}\nLocation: {Location}\nMood: {Mood}\nRating: {Rating}/5\n";
    }
}

class Journal
{
    private List<Entry> entries;

    public Journal()
    {
        entries = new List<Entry>();
    }

    public void AddEntry(string prompt, string response, string date, string location, string mood, int rating)
    {
        Entry newEntry = new Entry(prompt, response, date, location, mood, rating);
        entries.Add(newEntry);
    }

    public List<Entry> GetEntries()
    {
        return entries;
    }

    public string DisplayEntries()
    {
        string result = "";
        foreach (var entry in entries)
        {
            result += entry.ToString() + new string('-', 50) + "\n\n"; // Add extra newline for better spacing
        }
        return result;
    }

    public void SaveToFile(string filename)
    {
        using (StreamWriter writer = new StreamWriter(filename))
        {
            foreach (var entry in entries)
            {
                writer.WriteLine($"{entry.Date},{entry.Prompt},{entry.Response},{entry.Location},{entry.Mood},{entry.Rating}");
            }
        }
    }

    public void LoadFromFile(string filename)
    {
        entries.Clear();
        using (StreamReader reader = new StreamReader(filename))
        {
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine().Split(',');
                AddEntry(line[1], line[2], line[0], line[3], line[4], int.Parse(line[5]));
            }
        }
    }
}

class Program
{
    static void Main()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("Welcome to Your Journal App!");
        Console.WriteLine("----------------------------");

        Journal journal = new Journal();

        while (true)
        {
            Console.WriteLine("Select an option:");
            Console.WriteLine("1. Write a new entry");
            Console.WriteLine("2. Display the journal");
            Console.WriteLine("3. Save the journal to a file");
            Console.WriteLine("4. Load the journal from a file");
            Console.WriteLine("5. Exit");

            int choice = 0;
            bool isValidChoice = false;

            while (!isValidChoice)
            {
                Console.Write("Enter your choice (1-5): ");
                isValidChoice = int.TryParse(Console.ReadLine(), out choice) && choice >= 1 && choice <= 5;

                if (!isValidChoice)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid choice. Please enter a number between 1 and 5.");
                    Console.ResetColor();
                }
            }

            Console.WriteLine(); // Add a line for better spacing

            switch (choice)
            {
                case 1:
                    Console.WriteLine("Select a prompt:");
                    string prompt = GetRandomPrompt();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(prompt);
                    Console.ResetColor();

                    Console.Write("Your response: ");
                    string response = Console.ReadLine();

<<<<<<< HEAD
                    Console.Write("Where are you today: ");
=======
                    Console.Write("Where are you today?: ");
>>>>>>> 3690fe5 (Finished W02 Prove Project)
                    string location = Console.ReadLine();

                    Console.Write("Enter your mood: ");
                    string mood = Console.ReadLine();

                    Console.Write("Rate your day (1-5): ");
                    int rating = int.Parse(Console.ReadLine());

                    journal.AddEntry(prompt, response, DateTime.Now.ToString(), location, mood, rating);

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(GetEncouragementMessage(rating));
                    Console.WriteLine(GetSmileyFace(rating));
                    Console.ResetColor();
                    break;

                case 2:
                    List<Entry> entries = journal.GetEntries();
                    if (entries.Count > 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine("Journal Entries:");
                        Console.ResetColor();
                        Console.WriteLine(journal.DisplayEntries());
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Your journal is empty. Write some entries first!\n");
                        Console.ResetColor();
                    }
                    break;

                case 3:
                    Console.Write("Enter a filename to save the journal: ");
                    string saveFilename = Console.ReadLine();
                    journal.SaveToFile(saveFilename);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nJournal saved successfully!\n");
                    Console.ResetColor();
                    break;

                case 4:
                    Console.Write("Enter a filename to load the journal: ");
                    string loadFilename = Console.ReadLine();
                    journal.LoadFromFile(loadFilename);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nJournal loaded successfully!\n");
                    Console.ResetColor();
                    break;

                case 5:
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("Exiting the Journal App. Goodbye!");
                    Console.ResetColor();
                    Environment.Exit(0);
                    break;

                default:
                    break;
            }
        }
    }

    static string GetRandomPrompt()
    {
        string[] prompts = {
            "Who was the most interesting person I interacted with today?",
            "What was the best part of my day?",
            "How did I see the hand of the Lord in my life today?",
            "What was the strongest emotion I felt today?",
            "If I had one thing I could do over today, what would it be?",
            "Describe a place that made an impact on you today.",
            "Share a moment when you felt truly content.",
            "How did you overcome a challenge today?",
            "What music or book influenced your day?",
            "Reflect on a small act of kindness you witnessed or experienced."
        };

        Random random = new Random();
        int index = random.Next(prompts.Length);
        return prompts[index];
    }

    static string GetEncouragementMessage(int rating)
    {
        if (rating > 3)
        {
            return "That's great! Keep up the positive vibes!";
        }
        else
        {
            return "Why is that? Reflect on your day and find ways to improve.";
        }
    }

    static string GetSmileyFace(int rating)
    {
        switch (rating)
        {
            case 1:
                return ":( Sad day!";
            case 2:
                return ":| Not bad, but could be better.";
            case 3:
                return ":) Decent day!";
            case 4:
                return ":D Good day!";
            case 5:
                return ":) :D Fantastic day!";
            default:
                return "";
        }
    }
}
