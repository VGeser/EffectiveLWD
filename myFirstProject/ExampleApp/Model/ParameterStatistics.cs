using System;
using System.ComponentModel;

namespace ExampleApp.Model;

public class ParameterStatistics : INotifyPropertyChanged
{
    private readonly double _pointsPerHour;
    public ParameterStatistics(double pointsPerHour)
    {
        PointsPerHour = pointsPerHour;
    }

    public double PointsPerHour
    {
        get => Math.Round(_pointsPerHour, 3);
        private init => _pointsPerHour = value;
    }
    public event PropertyChangedEventHandler? PropertyChanged;
}