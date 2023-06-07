using System.Collections.Generic;

namespace ExampleApp.Model;

public class SimulationStatistics
{
    public int TotalMessages { get; set; }
    public int FileLength { get; set; }

    public Dictionary<string, ParameterStatistics> ParameterStats { get; set; } = new();

}