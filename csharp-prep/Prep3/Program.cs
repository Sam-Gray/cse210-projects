using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to the Guess My Number game!");
        
        Random random = new Random();
        int magicNumber = random.Next(1, 101); // Generate a random number between 1 and 100

        int userGuess;
        int attempts = 0;

        do
        {
            Console.Write("What is your guess? ");
            if (int.TryParse(Console.ReadLine(), out userGuess))
            {
                if (userGuess < magicNumber)
                {
                    Console.WriteLine("Higher");
                }
                else if (userGuess > magicNumber)
                {
                    Console.WriteLine("Lower");
                }
                else
                {
                    Console.WriteLine("You guessed it!");
                }
                attempts++;
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
            }
        } while (userGuess != magicNumber);

        Console.WriteLine($"It took you {attempts} attempts to guess the magic number.");
        
        Console.Write("Do you want to play again? (yes/no): ");
        string playAgain = Console.ReadLine().ToLower();

        if (playAgain == "yes")
        {
            Main(args); // Restart the game
        }
        else
        {
            Console.WriteLine("Thank you for playing the Guess My Number game!");
        }
    }
}

