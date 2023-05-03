namespace LearningCSharp;

public class MainClass
{
    public static void Main()
    {
        EncodingHistogram histogram = new EncodingHistogramCreatorWithNormal().Create(5, 2, 22,188, 100, 16, 3);
        foreach (int i in histogram.getHistogram().Keys)
        {
            Console.Write("{" + i + ": ");
            foreach (SetPosition value in histogram.getHistogram()[i])
            {
                Console.Write(value + " ");
            }
            Console.WriteLine("}");
        }

        foreach (var num in histogram.Lookup(21, 16, 3))
        {
            Console.Write(num + " ");
        }
    }
}