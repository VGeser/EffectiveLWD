namespace SimulatorSubsystem;

public class Slicer
{
    private readonly Dictionary<String, Double?[]> _curves;

    public Slicer(Dictionary<String, Double?[]> curves)
    {
        _curves = curves;
    }

    public int ContainsNull(String mnem)
    {
        return Array.IndexOf(_curves[mnem], null);
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
        using var enumerator = _curves.Values.GetEnumerator();
        enumerator.MoveNext();
        var current = enumerator.Current;
        return current.GetLength(0);
    }
}