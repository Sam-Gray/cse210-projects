using System;
using System.Collections.Generic;

// Base Activity class
class Activity
{
    private DateTime date;
    private int minutes;

    public Activity(DateTime date, int minutes)
    {
        this.date = date;
        this.minutes = minutes;
    }

    public int GetMinutes()
    {
        return minutes;
    }

    public virtual double GetDistance()
    {
        return 0; // Default implementation for activities without distance
    }

    public virtual double GetSpeed()
    {
        return 0; // Default implementation for activities without speed
    }

    public virtual double GetPace()
    {
        return 0; // Default implementation for activities without pace
    }

    public virtual string GetSummary()
    {
        return $"{date.ToShortDateString()} - {GetType().Name} ({minutes} min)";
    }
}

// Running class derived from Activity
class Running : Activity
{
    private double distance;

    public Running(DateTime date, int minutes, double distance)
        : base(date, minutes)
    {
        this.distance = distance;
    }

    public override double GetDistance()
    {
        return distance;
    }

    public override double GetSpeed()
    {
        return distance / (GetMinutes() / 60);
    }

    public override double GetPace()
    {
        return GetMinutes() / distance;
    }

    public override string GetSummary()
    {
        return $"{base.GetSummary()} - Distance: {distance} miles, Speed: {GetSpeed()} mph, Pace: {GetPace()} min per mile";
    }
}

// StationaryBicycle class derived from Activity
class StationaryBicycle : Activity
{
    private double speed;

    public StationaryBicycle(DateTime date, int minutes, double speed)
        : base(date, minutes)
    {
        this.speed = speed;
    }

    public override double GetSpeed()
    {
        return speed;
    }

    public override double GetPace()
    {
        return 60 / speed;
    }

    public override string GetSummary()
    {
        return $"{base.GetSummary()} - Speed: {speed} kph, Pace: {GetPace()} min per km";
    }
}

// Swimming class derived from Activity
class Swimming : Activity
{
    private int laps;

    public Swimming(DateTime date, int minutes, int laps)
        : base(date, minutes)
    {
        this.laps = laps;
    }

    public override double GetDistance()
    {
        return laps * 50 / 1000; // Convert laps to kilometers
    }

    public override double GetSpeed()
    {
        return GetDistance() / (GetMinutes() / 60);
    }

    public override double GetPace()
    {
        return GetMinutes() / GetDistance();
    }

    public override string GetSummary()
    {
        return $"{base.GetSummary()} - Distance: {GetDistance()} km, Speed: {GetSpeed()} kph, Pace: {GetPace()} min per km";
    }
}

class Program
{
    static void Main()
    {
        List<Activity> activities = new List<Activity>();

        do
        {
            Console.Clear(); // Clear the console before displaying the menu

            Console.WriteLine("Fitness Center Activity Tracker\n");
            Console.WriteLine("1. Record Running Activity");
            Console.WriteLine("2. Record Stationary Bicycle Activity");
            Console.WriteLine("3. Record Swimming Activity");
            Console.WriteLine("4. View Activity Summaries");
            Console.WriteLine("5. Exit");

            int choice;
            do
            {
                Console.Write("Enter your choice (1-5): ");
            } while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 5);

            Console.Clear(); // Clear the console before displaying the chosen option

            switch (choice)
            {
                case 1:
                    RecordRunningActivity(activities);
                    break;
                case 2:
                    RecordStationaryBicycleActivity(activities);
                    break;
                case 3:
                    RecordSwimmingActivity(activities);
                    break;
                case 4:
                    ViewActivitySummaries(activities);
                    Console.WriteLine("\nPress Enter to return to the main menu...");
                    Console.ReadLine();
                    break;
                case 5:
                    Environment.Exit(0); // Exit the program
                    break;
                default:
                    break;
            }

        } while (true);
    }

    static void RecordRunningActivity(List<Activity> activities)
    {
        Console.WriteLine("Record Running Activity\n");

        DateTime date = GetDate();
        int minutes = GetMinutes();
        double distance = GetDoubleInput("Enter the distance (miles): ");

        Running runningActivity = new Running(date, minutes, distance);
        activities.Add(runningActivity);

        Console.WriteLine("\nRunning activity recorded successfully!");
    }

    static void RecordStationaryBicycleActivity(List<Activity> activities)
    {
        Console.WriteLine("Record Stationary Bicycle Activity\n");

        DateTime date = GetDate();
        int minutes = GetMinutes();
        double speed = GetDoubleInput("Enter the speed (kph): ");

        StationaryBicycle bicycleActivity = new StationaryBicycle(date, minutes, speed);
        activities.Add(bicycleActivity);

        Console.WriteLine("\nStationary bicycle activity recorded successfully!");
    }

    static void RecordSwimmingActivity(List<Activity> activities)
    {
        Console.WriteLine("Record Swimming Activity\n");

        DateTime date = GetDate();
        int minutes = GetMinutes();
        int laps = GetIntInput("Enter the number of laps: ");

        Swimming swimmingActivity = new Swimming(date, minutes, laps);
        activities.Add(swimmingActivity);

        Console.WriteLine("\nSwimming activity recorded successfully!");
    }

    static void ViewActivitySummaries(List<Activity> activities)
    {
        Console.WriteLine("Activity Summaries\n");

        foreach (Activity activity in activities)
        {
            Console.WriteLine(activity.GetSummary());
        }
    }

    static DateTime GetDate()
    {
        DateTime date;
        do
        {
            Console.Write("Enter the date (MM/dd/yyyy): ");
        } while (!DateTime.TryParse(Console.ReadLine(), out date));

        return date;
    }

    static int GetMinutes()
    {
        int minutes;
        do
        {
            Console.Write("Enter the duration in minutes: ");
        } while (!int.TryParse(Console.ReadLine(), out minutes) || minutes <= 0);

        return minutes;
    }

    static int GetIntInput(string prompt)
    {
        int value;
        do
        {
            Console.Write(prompt);
        } while (!int.TryParse(Console.ReadLine(), out value));

        return value;
    }

    static double GetDoubleInput(string prompt)
    {
        double value;
        do
        {
            Console.Write(prompt);
        } while (!double.TryParse(Console.ReadLine(), out value));

        return value;
    }
}