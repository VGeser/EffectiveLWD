namespace SimulatorSubsystem;

public class Slicer
{
    private readonly Dictionary<String, Double?[]> _curves;

    public Slicer(Dictionary<String, Double?[]> curves)
    {
        _curves = curves;
    }

    public Dictionary<String, Double?> GetSlice(int n)
    {
        Dictionary<String, Double?> res = new Dictionary<string, double?>();
        foreach (KeyValuePair<String, Double?[]> pair in _curves)
        {
            res.Add(pair.Key, pair.Value[n]);
        }

        return res;
    }

    public int GetSize()
    {
        return _curves.Values.GetEnumerator().Current.GetLength(0);
    }
}