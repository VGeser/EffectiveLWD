namespace SimulatorSubsystem;

public class EncodingHistogram
{
    private readonly Dictionary<Int32, List<SetPosition>> _histogram;
    private readonly Int32 _binSize;
    private readonly Int32 _symbols;
    private readonly Int32 _radix;
    
    public EncodingHistogram(Int32 binSize, Dictionary<Int32, List<SetPosition>> histogram, Int32 radix, Int32 symbols)
    {
        _binSize = binSize;
        _histogram = histogram;
        _radix = radix;
        _symbols = symbols;
    }
    public List<Int32> Lookup(Int32 repres)
    {
        int histogramKey = Int32.MinValue;
        foreach (int minimalForBin in _histogram.Keys)
        {
            if (repres >= minimalForBin && repres < minimalForBin + _binSize)
            {
                histogramKey = minimalForBin;
            }
        }

        int index = repres - histogramKey;

        if (histogramKey == Int32.MinValue)
        {
            return new List<int>();
        }

        foreach (SetPosition position in _histogram[histogramKey])
        {
            if (position.Count <= index)
            {
                index -= position.Count;
                continue;
            }

            List<List<Int32>> encodings = Util.GetEncodings(new List<Int32>(), _symbols, position.Sum, _radix);
            return encodings[position.IndexFrom + index];
        }

        return new List<Int32>();
    }

    public int Decode(List<int> encoded)
    {
        int sum = encoded.Sum();
                
        int index = -1;
        List<List<int>> possibleEncodings = Util.GetEncodings(new List<int>(), _symbols, sum, _radix);
        for (int i = 0; i < possibleEncodings.Count; i++)
        {
            if (possibleEncodings[i].SequenceEqual(encoded))
            {
                index = i;
                break;
            }
        }
        
        foreach (var key in _histogram.Keys)
        {
            List<SetPosition> value = _histogram[key];
            int passed = 0;
            foreach (var toCheck in value)
            {
                if (toCheck.Sum == sum && toCheck.IndexFrom + toCheck.Count > index)
                {
                    return key + passed + index - toCheck.IndexFrom;
                }
                passed += toCheck.Count;
            }
        }
        
        return Int32.MinValue;
    }
}