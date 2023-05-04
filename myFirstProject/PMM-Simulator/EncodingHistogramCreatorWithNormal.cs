namespace SimulatorSubsystem;

public class EncodingHistogramCreatorWithNormal
{
    public EncodingHistogram Create(Int32 binSize, Int32 step, Int32 rangeLowerRaw, Int32 rangeUpperRaw, Int32 centerBinStart,
        Int32 radix, Int32 symbols)
    {
        Dictionary<Int32, List<SetPosition>> histogram = new Dictionary<int, List<SetPosition>>();

        int rangeLower = rangeLowerRaw;
        while (rangeLower % (binSize * step) > 0)
        {
            rangeLower--;
        }
        
        int rangeUpper = rangeUpperRaw;
        while (rangeUpper % (binSize * step) > 0)
        {
            rangeUpper++;
        }
        
        SetPosition current = new SetPosition(0, 0, 0);
        int totalFromCurrent = 0;
        List<List<Int32>> allEncodings = Util.GetEncodings(new List<int>(), symbols, current.Sum, radix);
        int nBins = (rangeUpper - rangeLower) / binSize / step + 1;
        for (int j = 0; j < nBins; j++)
        {
            int centerFrom = centerBinStart + (Int32)Math.Pow(-1, j) * binSize * step * ((j + 1) / 2);
            if (centerFrom < rangeLower || centerFrom > rangeUpper)
            {
                Console.WriteLine("SKIP " + centerFrom);
                nBins++;
                continue;
            }
            histogram.Add(centerFrom, new List<SetPosition>());
            for (int i = 0; i < binSize; i++)
            {
                current.Count++;
                totalFromCurrent++;
                if (current.Count + current.IndexFrom == allEncodings.Count)
                {
                    histogram[centerFrom].Add((SetPosition)current.Clone());
                    current.Sum++;
                    current.Count = 0;
                    current.IndexFrom = 0;
                    totalFromCurrent = 0;
                    allEncodings = Util.GetEncodings(new List<int>(), symbols, current.Sum, radix);
                }
                else if (i == binSize - 1)
                {
                    histogram[centerFrom].Add((SetPosition)current.Clone());
                    current.IndexFrom = totalFromCurrent;
                    current.Count = 0;
                }
            }
        }

        return new EncodingHistogram(binSize, step, histogram);
    }
}