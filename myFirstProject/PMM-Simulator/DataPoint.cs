namespace SimulatorSubsystem;

public class DataPoint
{
    public double XValue { get; set; }
    public double YValue { get; set; }

    public DataPoint(double xValue, double yValue)
    {
        XValue = xValue;
        YValue = yValue;
    }
}