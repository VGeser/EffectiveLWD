namespace SimulatorSubsystem;

public class TimeData
{
    public int Synchro { get; set; }
    public int Tick { get; set; }
    public int Start { get; set; }

    public TimeData(int synchro, int tick, int start)
    {
        Synchro = synchro;
        Tick = tick;
        Start = start;
    }
}