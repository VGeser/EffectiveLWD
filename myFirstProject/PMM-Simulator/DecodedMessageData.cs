namespace SimulatorSubsystem;

public class DecodedMessageData
{
    private List<Dictionary<String, Double>> _messages;
    private List<int> _timesOfFinish;

    public DecodedMessageData(int size)
    {
        _timesOfFinish = new List<int>(size);
        _messages = new List<Dictionary<string, double>>(size);
    }

    public void AddMessage(Dictionary<String, Double> message, int timeOfFinish)
    {
        _messages.Add(message);
        _timesOfFinish.Add(timeOfFinish);
    }

    public List<Dictionary<String, Double>> GetMessages()
    {
        return _messages;
    }

    public List<int> GetTimes()
    {
        return _timesOfFinish;
    }

    public int GetSize()
    {
        return _messages.Capacity;
    }
}