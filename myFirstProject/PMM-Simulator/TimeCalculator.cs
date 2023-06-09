namespace SimulatorSubsystem;

public class TimeCalculator
{
    private readonly TimeData _timeData;

    public TimeCalculator(TimeData timeData)
    {
        _timeData = timeData;
    }

    public int CalculateTime(String message)
    {
        int sum = 0;
        foreach (char symbol in message)
        {
            if (symbol == 'S')
            {
                sum += _timeData.Synchro;
                continue;
            }

            sum += _timeData.Start;
            int ticks = symbol is >= '0' and <= '9' ? symbol - '0' : symbol - 'a' + 10;
            sum += ticks * _timeData.Tick;
        }

        return sum;
    }
}