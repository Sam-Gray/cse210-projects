using System;

// Address class to encapsulate address details
class Address
{
    public string Street { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string ZipCode { get; set; }

    public override string ToString()
    {
        return $"{Street}, {City}, {State} {ZipCode}";
    }
}

// Base Event class
class Event
{
    private string title;
    private string description;
    private DateTime date;
    private TimeSpan time;
    private Address address;

    public Event(string title, string description, DateTime date, TimeSpan time, Address address)
    {
        this.title = title;
        this.description = description;
        this.date = date;
        this.time = time;
        this.address = address;
    }

    public string GetStandardDetails()
    {
        return $"{title}\n{description}\nDate: {date.ToShortDateString()}\nTime: {time}\nAddress: {address}";
    }

    public virtual string GetFullDetails()
    {
        return GetStandardDetails();
    }

    public string GetShortDescription()
    {
        return $"{GetType().Name} - {title}\nDate: {date.ToShortDateString()}";
    }
}

// Lecture class derived from Event
class Lecture : Event
{
    private string speaker;
    private int capacity;

    public Lecture(string title, string description, DateTime date, TimeSpan time, Address address, string speaker, int capacity)
        : base(title, description, date, time, address)
    {
        this.speaker = speaker;
        this.capacity = capacity;
    }

    public override string GetFullDetails()
    {
        return $"{base.GetFullDetails()}\nSpeaker: {speaker}\nCapacity: {capacity}";
    }
}

// Reception class derived from Event
class Reception : Event
{
    private string rsvpEmail;

    public Reception(string title, string description, DateTime date, TimeSpan time, Address address, string rsvpEmail)
        : base(title, description, date, time, address)
    {
        this.rsvpEmail = rsvpEmail;
    }

    public override string GetFullDetails()
    {
        return $"{base.GetFullDetails()}\nRSVP Email: {rsvpEmail}";
    }
}

// OutdoorGathering class derived from Event
class OutdoorGathering : Event
{
    private string weatherStatement;

    public OutdoorGathering(string title, string description, DateTime date, TimeSpan time, Address address, string weatherStatement)
        : base(title, description, date, time, address)
    {
        this.weatherStatement = weatherStatement;
    }

    public override string GetFullDetails()
    {
        return $"{base.GetFullDetails()}\nWeather: {weatherStatement}";
    }
}

class Program
{
    static void Main()
    {
        do
        {
            Console.Clear(); // Clear the console before displaying the menu

            // Create instances of each event type
            Address address1 = new Address { Street = "123 Main St", City = "Cityville", State = "Stateville", ZipCode = "12345" };
            Lecture lectureEvent = new Lecture("Interesting Lecture", "A fascinating talk about something", DateTime.Now, new TimeSpan(14, 0, 0), address1, "John Doe", 100);

            Address address2 = new Address { Street = "456 Oak St", City = "Townsville", State = "Stateton", ZipCode = "54321" };
            Reception receptionEvent = new Reception("Networking Reception", "An evening of networking and socializing", DateTime.Now.AddDays(7), new TimeSpan(18, 30, 0), address2, "rsvp@example.com");

            Address address3 = new Address { Street = "789 Pine St", City = "Villageville", State = "Townston", ZipCode = "98765" };
            OutdoorGathering outdoorEvent = new OutdoorGathering("Summer Picnic", "Enjoy a day outdoors with friends and family", DateTime.Now.AddDays(14), new TimeSpan(12, 0, 0), address3, "Sunny with a chance of clouds");

            // Display menu to let the user choose an event
            Console.WriteLine("Choose an Event:");
            Console.WriteLine("1. Lecture Event");
            Console.WriteLine("2. Reception Event");
            Console.WriteLine("3. Outdoor Gathering Event");
            Console.WriteLine("4. Exit");

            // Get user input for event choice
            int choice;
            do
            {
                Console.Write("Enter your choice (1-4): ");
            } while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 4);

            if (choice == 4)
            {
                // Exit the program if the user chooses to exit
                break;
            }

            Console.Clear(); // Clear the console before displaying the marketing messages

            // Display marketing messages based on user's choice
            Console.WriteLine("=====================================");
            Console.WriteLine("         EVENT MARKETING MESSAGES     ");
            Console.WriteLine("=====================================\n");

            switch (choice)
            {
                case 1:
                    DisplayEventMarketingMessages("Lecture Event", lectureEvent);
                    break;
                case 2:
                    DisplayEventMarketingMessages("Reception Event", receptionEvent);
                    break;
                case 3:
                    DisplayEventMarketingMessages("Outdoor Gathering Event", outdoorEvent);
                    break;
                default:
                    break;
            }

            Console.WriteLine("\nPress Enter to return to the main menu...");
            Console.ReadLine();
        } while (true);
    }

    // Helper method to display marketing messages for an event
    static void DisplayEventMarketingMessages(string eventType, Event eventObj)
    {
        Console.WriteLine($"{eventType} Marketing Messages:\n");
        Console.WriteLine($"Standard Details:\n{eventObj.GetStandardDetails()}\n");
        Console.WriteLine($"Full Details:\n{eventObj.GetFullDetails()}\n");
        Console.WriteLine($"Short Description:\n{eventObj.GetShortDescription()}\n");
    }
}