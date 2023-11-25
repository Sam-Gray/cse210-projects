public class WritingAssignment : Assignment
{
    private string _writingInformation;

    public WritingAssignment(string studentName, string topic, string writingInformation)
        : base(studentName, topic)
    {
        _writingInformation = writingInformation;
    }

    public string GetWritingInformation()
    {
        return $"{GetStudentName()} - {_writingInformation}";
    }

    // Public method to access private _studentName from the base class
    public string GetStudentName()
    {
        return _studentName;
    }
}