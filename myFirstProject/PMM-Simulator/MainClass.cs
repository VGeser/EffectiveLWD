namespace LearningCSharp;

public class MainClass
{
    public static void Main()
    {
        LasReader thing = new LasReader();
        Dictionary<String, Double?[]> map = thing.SaveDict(@"C:\Users\Synchro\Downloads\Считанные.mpp.las", "utf-8");
        foreach (KeyValuePair<String, Double?[]> pair in map)
        {
            Console.Write(pair.Key + " ");
            Console.WriteLine(pair.Value.Length);
        }
    }
}