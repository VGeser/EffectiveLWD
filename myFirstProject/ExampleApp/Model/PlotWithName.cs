using System.Collections.Generic;
using LiveChartsCore;

namespace ExampleApp.Model;

public class PlotWithName
{
    public string Name { get; set; }

    public List<ISeries> Plot { get; set; }
}