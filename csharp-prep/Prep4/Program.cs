class Program
{
    static void Main()
    {
        // Test MathAssignment
        MathAssignment mathAssignment = new MathAssignment("Samuel Benett", "Multiplication", "Section 7.3 Problems 8-20");
        Console.WriteLine(mathAssignment.GetSummary());
        Console.WriteLine(mathAssignment.GetHomeworkList());

        // Test WritingAssignment
        WritingAssignment writingAssignment = new WritingAssignment("Roberto Rodriguez", "Fractions", "The causes of WWII by Mary Waters");
        Console.WriteLine(writingAssignment.GetSummary());
        Console.WriteLine(writingAssignment.GetWritingInformation());
    }
}