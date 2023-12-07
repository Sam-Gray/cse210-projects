using System;
using System.Collections.Generic;
using System.IO;

class Goal
{
    public string Name { get; set; }
    public int Points { get; set; }

    public Goal(string name, int points)
    {
        Name = name;
        Points = points;
    }

    public virtual void DisplayProgress()
    {
        Console.WriteLine($"Name: {Name}\nPoints: {Points}");
    }

    public virtual void RecordEvent()
    {
        Console.WriteLine($"Event recorded for {Name}! +{Points} points");
    }
}

class SimpleGoal : Goal
{
    public SimpleGoal(string name, int points) : base(name, points)
    {
    }
}

class ChecklistGoal : Goal
{
    public int CompletedItems { get; set; }
    public int TotalItems { get; set; }

    public ChecklistGoal(string name, int points, int totalItems) : base(name, points)
    {
        TotalItems = totalItems;
        CompletedItems = 0;
    }

    public void MarkItemComplete()
    {
        if (CompletedItems < TotalItems)
        {
            CompletedItems++;
            Points += 2; // Bonus points for completing checklist item
        }
    }

    public override void DisplayProgress()
    {
        Console.WriteLine($"Name: {Name}\nPoints: {Points}");
        Console.WriteLine($"Progress: [{new string('X', CompletedItems)}{new string(' ', TotalItems - CompletedItems)}]");
    }

    public override void RecordEvent()
    {
        Console.WriteLine($"Event recorded for {Name}! +{Points} points (Checklist Goal)");
    }
}

class EternalGoal : Goal
{
    public EternalGoal(string name, int points) : base(name, points)
    {
    }

    public override void RecordEvent()
    {
        Console.WriteLine($"Event recorded for {Name}! +{Points} points (Eternal Goal)");
    }
}

class Program
{
    static List<Goal> goals = new List<Goal>();

    static void Main()
    {
        LoadGoals(); // Load goals from file

        int choice;
        do
        {
            DisplayMainMenu();
            choice = GetChoice();

            switch (choice)
            {
                case 1:
                    AddSimpleGoal();
                    break;
                case 2:
                    AddChecklistGoal();
                    break;
                case 3:
                    AddEternalGoal();
                    break;
                case 4:
                    SaveGoals();
                    break;
            }
        } while (choice != 5);
    }

    static void DisplayMainMenu()
    {
        Console.Clear();
        Console.WriteLine("Eternal Quest Program");
        Console.WriteLine("1. Add a Simple Goal");
        Console.WriteLine("2. Add a Checklist Goal");
        Console.WriteLine("3. Add an Eternal Goal");
        Console.WriteLine("4. Save Goals");
        Console.WriteLine("5. Quit");
        Console.Write("Enter your choice: ");
    }

    static int GetChoice()
    {
        int choice;
        while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 5)
        {
            Console.Write("Invalid choice. Please enter a number between 1 and 5: ");
        }
        return choice;
    }

    static void AddSimpleGoal()
    {
        Console.Clear();
        Console.WriteLine("Add a Simple Goal");

        Console.Write("Enter goal name: ");
        string name = Console.ReadLine();

        Console.Write("Enter points for the goal: ");
        int points;
        while (!int.TryParse(Console.ReadLine(), out points) || points < 1)
        {
            Console.Write("Invalid points. Please enter a positive integer: ");
        }

        goals.Add(new SimpleGoal(name, points));

        Console.WriteLine("Goal added successfully!");
        Console.ReadKey();
    }

    static void AddChecklistGoal()
    {
        Console.Clear();
        Console.WriteLine("Add a Checklist Goal");

        Console.Write("Enter goal name: ");
        string name = Console.ReadLine();

        Console.Write("Enter total items for the checklist: ");
        int totalItems;
        while (!int.TryParse(Console.ReadLine(), out totalItems) || totalItems < 1)
        {
            Console.Write("Invalid total items. Please enter a positive integer: ");
        }

        Console.Write("Enter points for the goal: ");
        int points;
        while (!int.TryParse(Console.ReadLine(), out points) || points < 1)
        {
            Console.Write("Invalid points. Please enter a positive integer: ");
        }

        goals.Add(new ChecklistGoal(name, points, totalItems));

        Console.WriteLine("Goal added successfully!");
        Console.ReadKey();
    }

    static void AddEternalGoal()
    {
        Console.Clear();
        Console.WriteLine("Add an Eternal Goal");

        Console.Write("Enter goal name: ");
        string name = Console.ReadLine();

        Console.Write("Enter points for the goal: ");
        int points;
        while (!int.TryParse(Console.ReadLine(), out points) || points < 1)
        {
            Console.Write("Invalid points. Please enter a positive integer: ");
        }

        goals.Add(new EternalGoal(name, points));

        Console.WriteLine("Goal added successfully!");
        Console.ReadKey();
    }

    static void SaveGoals()
    {
        Console.Clear();
        Console.WriteLine("Save Goals");

        using (StreamWriter writer = new StreamWriter("Goals.txt"))
        {
            foreach (var goal in goals)
            {
                if (goal is SimpleGoal simpleGoal)
                {
                    writer.WriteLine($"{nameof(SimpleGoal)},{simpleGoal.Name},{simpleGoal.Points}");
                }
                else if (goal is ChecklistGoal checklistGoal)
                {
                    writer.WriteLine($"{nameof(ChecklistGoal)},{checklistGoal.Name},{checklistGoal.Points},{checklistGoal.TotalItems}");
                }
                else if (goal is EternalGoal eternalGoal)
                {
                    writer.WriteLine($"{nameof(EternalGoal)},{eternalGoal.Name},{eternalGoal.Points}");
                }
            }
        }

        Console.WriteLine("Goals saved to Goals.txt!");
        Console.ReadKey();
    }

    static void LoadGoals()
    {
        if (File.Exists("Goals.txt"))
        {
            using (StreamReader reader = new StreamReader("Goals.txt"))
            {
                while (!reader.EndOfStream)
                {
                    string[] goalData = reader.ReadLine().Split(',');
                    string goalType = goalData[0];
                    string name = goalData[1];
                    int points = int.Parse(goalData[2]);

                    switch (goalType)
                    {
                        case nameof(SimpleGoal):
                            goals.Add(new SimpleGoal(name, points));
                            break;
                        case nameof(ChecklistGoal):
                            int totalItems = int.Parse(goalData[3]);
                            goals.Add(new ChecklistGoal(name, points, totalItems));
                            break;
                        case nameof(EternalGoal):
                            goals.Add(new EternalGoal(name, points));
                            break;
                    }
                }
            }
        }
    }
}