namespace SimulatorSubsystem;

public class EncodedMessageData
{
    private List<String> _messages;
    private List<int> _timesOfFinish;

    public EncodedMessageData()
    {
        _messages = new List<string>();
        _timesOfFinish = new List<int>();
    }

    public void AddMessage(String message, int timeOfFinish)
    {
        _messages.Add(message);
        _timesOfFinish.Add(timeOfFinish);
    }

    public int GetSize()
    {
        return _messages.Count;
    }

    public String GetMessageByIndex(int index)
    {
        return _messages[index];
    }
    
    public int GetTimeByIndex(int index)
    {
        return _timesOfFinish[index];
    }
}