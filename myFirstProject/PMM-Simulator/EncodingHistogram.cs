namespace SimulatorSubsystem;

public class EncodingHistogram
{
    private Dictionary<Int32, List<SetPosition>> _histogram;
    private Int32 _binSize;
    private Int32 _step;
    
    public EncodingHistogram(Int32 binSize, Int32 step, Dictionary<Int32, List<SetPosition>> histogram)
    {
        _binSize = binSize;
        _step = step;
        _histogram = histogram;
    }
    public List<Int32> Lookup(Int32 repres, Int32 radix, Int32 symbols)
    {
        int histogramKey = Int32.MinValue;
        foreach (int minimalForBin in _histogram.Keys)
        {
            if (repres >= minimalForBin && repres < minimalForBin + _binSize * _step)
            {
                histogramKey = minimalForBin;
            }
        }

        int index = (repres - histogramKey) / _step;
        Console.WriteLine(index);

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

            List<List<Int32>> encodings = Util.GetEncodings(new List<Int32>(), symbols, position.Sum, radix);
            return encodings[position.IndexFrom + index];
        }

        return new List<Int32>();
    }

    public Dictionary<Int32, List<SetPosition>> getHistogram()
    {
        return _histogram;
    }
}