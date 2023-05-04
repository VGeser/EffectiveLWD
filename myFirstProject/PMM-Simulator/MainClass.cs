namespace SimulatorSubsystem;

public class MainClass
{
    public static void Main()
    {
        EncodingHistogramCreatorWithNormal creatorWithNormal = new EncodingHistogramCreatorWithNormal();
        EncodingHistogram histo = creatorWithNormal.Create(5, 3, 750, 773, 764, 16, 4);
        Dictionary<int, List<SetPosition>> list = histo.getHistogram();
        foreach (var key in list.Keys)
        {
            Console.WriteLine("K: " + key);
            foreach (var pos in list[key])
            {
                Console.WriteLine("V: " + pos);
            }
        }
    }
}