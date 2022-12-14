namespace LearningCSharp;

public class MainClass
{
    public static void Main()
    {
        Table table = new Table();

        (string, int)[] a0 = { ("WGHT", 3), ("POS", 3) };
        (string, int)[] a1 = { ("STP", 1), ("PRES", 4) };
        (string, int)[] a2 = { ("WGHT", 3), ("PRES", 4) };

        table.AddRule(new Entry(
            new Entry.ChoiceCondition(true, true, true),
            new Entry.ChoiceBoundaries(1, 0),
            new Entry.Parameters("S0", new List<(string, int)>(a0)),
            0));
        
        table.AddRule(new Entry(
            new Entry.ChoiceCondition(true, true, true),
            new Entry.ChoiceBoundaries(1, 3),
            new Entry.Parameters("S1", new List<(string, int)>(a1)),
            1));
        
        table.AddRule(new Entry(
            new Entry.ChoiceCondition(true, false, true),
            new Entry.ChoiceBoundaries(3, 0),
            new Entry.Parameters("S2", new List<(string, int)>(a2)),
            2));
        
        table.AddRule(new Entry(
            new Entry.ChoiceCondition(true, false, true),
            new Entry.ChoiceBoundaries(1, 0),
            new Entry.Parameters("S3", new List<(string, int)>(a0)),
            3));

        LasReader reader = new LasReader();
        Dictionary<string, double?[]> dictionary = reader.SaveDict(@"..\..\..\data\2022_09_21_21_18_13_277.gti.las", "utf-8");
        (int, int, int) times = (1000, 300, 400);
        (bool, bool, bool)[] states =
        {
            (true, true, true), (true, true, true), (true, true, true), (true, true, true),
            (true, true, true), (true, true, true), (true, true, true), (true, true, true),
            (true, true, true), (true, true, true), (true, true, true), (true, true, true),
            (true, true, true), (true, true, true), (true, true, true), (true, true, true),
            (true, false, true), (true, false, true), (true, false, true), (true, false, true),
            (true, false, true), (true, false, true), (true, false, true), (true, false, true),
            (true, false, true), (true, false, true), (true, false, true), (true, false, true),
            (true, false, true), (true, false, true), (true, false, true), (true, false, true)
        };
        
        
        Simulator simulator = new Simulator(table, dictionary);
        (int, int, int, string)[] result = simulator.Simulate(times, states, 250, 4000);

        foreach (var stepRes in result)
        {
            Console.WriteLine(stepRes);
        }
    }

}