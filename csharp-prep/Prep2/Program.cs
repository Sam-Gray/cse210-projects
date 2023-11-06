using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to the Grade Calculator!");

        Console.Write("Please enter your grade percentage: ");
        double gradePercentage;
        
        if (double.TryParse(Console.ReadLine(), out gradePercentage))
        {
            char letterGrade = 'F'; // Default to F
            
            if (gradePercentage >= 90)
                letterGrade = 'A';
            else if (gradePercentage >= 80)
                letterGrade = 'B';
            else if (gradePercentage >= 70)
                letterGrade = 'C';
            else if (gradePercentage >= 60)
                letterGrade = 'D';

            Console.WriteLine($"Your letter grade is: {letterGrade}");

            if (gradePercentage >= 70)
                Console.WriteLine("Congratulations! You passed the class.");
            else
                Console.WriteLine("You can do better next time. Keep working hard!");
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter a valid grade percentage.");
        }
    }
}