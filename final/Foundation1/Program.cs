using System;
using System.Collections.Generic;

class Comment
{
    public string CommenterName { get; set; }
    public string CommentText { get; set; }

    public Comment(string commenterName, string commentText)
    {
        CommenterName = commenterName;
        CommentText = commentText;
    }
}

class Video
{
    public string Title { get; set; }
    public string Author { get; set; }
    public int LengthSeconds { get; set; }
    private List<Comment> Comments { get; set; }

    public Video(string title, string author, int lengthSeconds)
    {
        Title = title;
        Author = author;
        LengthSeconds = lengthSeconds;
        Comments = new List<Comment>();
    }

    public void AddComment(string commenterName, string commentText)
    {
        Comment comment = new Comment(commenterName, commentText);
        Comments.Add(comment);
    }

    public int GetNumberOfComments()
    {
        return Comments.Count;
    }

    public void DisplayVideoInfo()
    {
        Console.Clear(); // Clear the console for each video
        Console.WriteLine($"Title: {Title}");
        Console.WriteLine($"Author: {Author}");
        Console.WriteLine($"Length (seconds): {LengthSeconds}");
        Console.WriteLine($"Number of Comments: {GetNumberOfComments()}");

        if (GetNumberOfComments() > 0)
        {
            Console.WriteLine("\nComments:");
            foreach (Comment comment in Comments)
            {
                Console.WriteLine($" - {comment.CommenterName}: {comment.CommentText}");
            }
        }

        Console.WriteLine("\nPress Enter to view the next video...");
        Console.ReadLine();
    }
}

class Program
{
    static void Main()
    {
        // Creating videos and adding comments
        Video video1 = new Video("Introduction to C#", "C# Guru", 300);
        video1.AddComment("Coder123", "Great tutorial!");
        video1.AddComment("TechEnthusiast", "C# is amazing!");

        Video video2 = new Video("Web Development Basics", "WebWizard", 500);
        video2.AddComment("DevNewbie", "This is helpful, thanks!");
        video2.AddComment("CodeMaster", "Web development is cool.");

        Video video3 = new Video("Machine Learning Explained", "MLExpert", 600);
        video3.AddComment("DataScienceFan", "Fascinating concepts!");
        video3.AddComment("MLNewcomer", "Learning a lot from this.");

        // Putting videos in a list
        List<Video> videoList = new List<Video> { video1, video2, video3 };

        // Displaying video information and comments
        foreach (Video video in videoList)
        {
            video.DisplayVideoInfo();
        }
    }
}
