using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

class Goal
{
    public string Name { get; set; }
    public int Points { get; set; }

    public Goal(string name, int points)
    {
        Name = name;
        Points = points;
    }

    public virtual void DisplayProgress(int index)
    {
        Console.WriteLine($"{index + 1}. Name: {Name}\n   Points: {Points}");
    }

    public virtual void RecordEvent()
    {
        Console.WriteLine($"Event recorded for {Name}! +{Points} points");
    }

    public virtual void RecordEvent(ref int userPoints)
    {
        Console.WriteLine($"Event recorded for {Name}! +{Points} points");
    }
}

class SimpleGoal : Goal
{
    private bool isCompleted;

    public SimpleGoal(string name, int points) : base(name, points)
    {
        isCompleted = false;
    }

    public override void RecordEvent(ref int userPoints)
    {
        if (!isCompleted)
        {
            Console.WriteLine($"Event recorded for {Name}! +{Points} points (Simple Goal)");
            isCompleted = true;
            userPoints += Points; // Increment user points only if the goal is completed for the first time
        }
        else
        {
            Console.WriteLine($"{Name} has already been completed and cannot be completed again.");
        }
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

    private bool isFinalIteration = false;


    public void MarkItemComplete()
    {
        if (CompletedItems < TotalItems)
        {
            CompletedItems++;

            if (CompletedItems == TotalItems)
            {
                isFinalIteration = true;
                ApplyBonusPoints(); // Apply bonus points for the final iteration
            }
            //else
            //{
            //    Points += 2; // Bonus points for completing a non-final iteration
            //}
        }
    }

    private void ApplyBonusPoints()
    {
        Console.Write($"Do you want to apply bonus points for completing the final iteration of {Name}? (yes/no): ");
        string response = Console.ReadLine().ToLower();

        if (response == "yes")
        {
            Console.Write("Enter bonus points for the final iteration: ");
            int bonusPoints;
            while (!int.TryParse(Console.ReadLine(), out bonusPoints) || bonusPoints < 0)
            {
                Console.Write("Invalid bonus points. Please enter a non-negative integer: ");
            }

            Points += bonusPoints;
            Console.WriteLine($"Bonus points applied! Total points for {Name}: {Points}");
        }
    }
        
    

    public override void DisplayProgress(int index)
    {
        Console.WriteLine($"{index + 1}. Name: {Name}\n   Points: {Points}");
        Console.WriteLine($"   Progress: [{new string('X', CompletedItems)}{new string('.', TotalItems - CompletedItems)}] {CompletedItems}/{TotalItems}");
    }

    public override void RecordEvent()
    {
        Console.WriteLine($"Event recorded for {Name}! +{Points} points (Checklist Goal)");
        MarkItemComplete();

        // Simple animation for fireworks
        for (int i = 0; i < 5; i++)
        {
            Console.Clear();
            Console.WriteLine("Fireworks!");
            Console.WriteLine("".PadLeft(i * 2, ' ') + "*");
            Console.WriteLine("".PadLeft(i, ' ') + "|");
            Thread.Sleep(300);
        }
    }

}

class EternalGoal : Goal
{
    public EternalGoal(string name, int points) : base(name, points)
    {
    }

    public override void RecordEvent(ref int userPoints)
    {
        Console.WriteLine($"Event recorded for {Name}! +{Points} points (Eternal Goal)");
        userPoints += Points; // Increment user points every time the goal is recorded
    }
}


class Program
{
    static List<Goal> goals = new List<Goal>();
    static int userPoints = 0;

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
                case 5:
                    DisplayGoalsProgress();
                    break;
                case 6:
                    RecordEvent(ref userPoints);
                    break;
            }
        } while (choice != 7);
    }

    static void DisplayMainMenu()
    {
        Console.Clear();
        Console.WriteLine($"Eternal Quest Program - Total Points: {userPoints}");
        Console.WriteLine("1. Add a Simple Goal");
        Console.WriteLine("2. Add a Checklist Goal");
        Console.WriteLine("3. Add an Eternal Goal");
        Console.WriteLine("4. Save Goals");
        Console.WriteLine("5. Display Goals Progress");
        Console.WriteLine("6. Record Goal Progress");
        Console.WriteLine("7. Quit");
        Console.Write("Enter your choice: ");
    }

    static int GetChoice()
    {
        int choice;
        while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 7)
        {
            Console.Write("Invalid choice. Please enter a number between 1 and 7: ");
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

        Console.Write("Enter points for each completion of this goal: ");
        int points;
        while (!int.TryParse(Console.ReadLine(), out points) || points < 1)
        {
            Console.Write("Invalid points. Please enter a positive integer: ");
        }

        goals.Add(new ChecklistGoal(name, points, totalItems));
        //userPoints += points; // Increment user points

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

    static void DisplayGoalsProgress()
    {
        Console.Clear();
        Console.WriteLine("Goals Progress");

        for (int i = 0; i < goals.Count; i++)
        {
            goals[i].DisplayProgress(i);
            Console.WriteLine();
        }

        Console.ReadKey();
    }

    static void RecordEvent(ref int userPoints)
{
    Console.Clear();
    Console.WriteLine("Record Goal Progress");

    DisplayGoalsProgress();

    Console.Write("Enter the number of the goal to record progress on: ");
    if (int.TryParse(Console.ReadLine(), out int number) && number >= 1 && number <= goals.Count)
    {
        Goal selectedGoal = goals[number - 1];

        // Check the type of the goal and call the appropriate RecordEvent method
        if (selectedGoal is SimpleGoal simpleGoal)
        {
            simpleGoal.RecordEvent(ref userPoints);
        }
        else if (selectedGoal is EternalGoal eternalGoal)
        {
            eternalGoal.RecordEvent(ref userPoints);
        }
        else
        {
            // For other types of goals, call the general RecordEvent method
            selectedGoal.RecordEvent();
            userPoints += selectedGoal.Points;
        }
    }
    else
    {
        Console.WriteLine("Invalid number. Please enter a valid number.");
    }

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
                            userPoints += points; // Increment user points
                            break;
                        case nameof(ChecklistGoal):
                            int totalItems = int.Parse(goalData[3]);
                            goals.Add(new ChecklistGoal(name, points, totalItems));
                            userPoints += points; // Increment user points
                            break;
                        case nameof(EternalGoal):
                            goals.Add(new EternalGoal(name, points));
                            userPoints += points; // Increment user points
                            break;
                    }
                }
            }
        }
    }
}