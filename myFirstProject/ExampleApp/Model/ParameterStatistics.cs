namespace ExampleApp.Model;

public class ParameterStatistics
{
    public ParameterStatistics(double pointsPerHour)
    {
        PointsPerHour = pointsPerHour;
    }

    public double PointsPerHour { get; }
}