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
            result += entry.ToString() + new string('-', 50) + "\n"; // Add a line separator for better readability
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
        using (StreamReader reader = new StreamReader(filename))
        {
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine().Split(',');

                if (line.Length == 6 && !string.IsNullOrEmpty(line[5]))
                {
                    AddEntry(line[1], line[2], line[0], line[3], line[4], int.Parse(line[5]));
                }
                else
                {
                    // Handle the case where the rating is missing or empty
                    Console.WriteLine($"Skipping invalid entry: {string.Join(",", line)}");
                }
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
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(GetEncouragementStatement());
                    Console.ResetColor();

                    if (prompt.StartsWith("Rate your day"))
                    {
                        Console.Write("Why is that? ");
                        string ratingResponse = Console.ReadLine();
                        int rating;
                        while (!int.TryParse(ratingResponse, out rating) || rating < 1 || rating > 5)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("Invalid rating. Please enter a number between 1 and 5: ");
                            Console.ResetColor();
                            ratingResponse = Console.ReadLine();
                        }

                        journal.AddEntry(prompt, $"{response}\n{ratingResponse}", DateTime.Now.ToString(), "", "", rating);
                    }
                    else
                    {
                        journal.AddEntry(prompt, response, DateTime.Now.ToString(), "", "", 0);
                    }
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

    static string GetEncouragementStatement()
    {
        string[] statements = {
            "Thanks for sharing!",
            "That's great! Keep it up!",
            "Very thoughtful!",
            "Reflect on your day.",
        };

        Random random = new Random();
        int index = random.Next(statements.Length);
        return statements[index];
    }
}