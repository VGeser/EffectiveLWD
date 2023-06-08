namespace SimulatorSubsystem;

public class EncodingHistogramCreatorWithNormal
{
    public static EncodingHistogram Create(Int32 binSize, Double step, Double rangeLowerRaw, Double rangeUpperRaw,
        Double centerBinStartRaw, Int32 radix, Int32 symbols)
    {
        Dictionary<Int32, List<SetPosition>> histogram = new Dictionary<int, List<SetPosition>>();

        int centerBinStart = (int)Math.Round(centerBinStartRaw / step);
        int rangeLower = (int)Math.Round(rangeLowerRaw / step);
        int rangeUpper = (int)Math.Round(rangeUpperRaw / step);
        int nBins = 1;
        
        
        int rangeLowerFinal = centerBinStart;
        while (rangeLowerFinal > rangeLower)
        {
            nBins++;
            rangeLowerFinal -= binSize;
        }

        int rangeUpperFinal = centerBinStart + binSize - 1;
        while (rangeUpperFinal < rangeUpper)
        {
            nBins++;
            rangeUpperFinal += binSize;
        }

        rangeLower = rangeLowerFinal;
        rangeUpper = rangeUpperFinal;

        SetPosition current = new SetPosition(0, 0, 0);
        int totalFromCurrent = 0;
        List<List<Int32>> allEncodings = Util.GetEncodings(new List<int>(), symbols, current.Sum, radix);
        for (int j = 0; j < nBins; j++)
        {
            int centerFrom = centerBinStart + (Int32)Math.Pow(-1, j) * binSize * ((j + 1) / 2);
            if (centerFrom < rangeLower || centerFrom > rangeUpper)
            {
                nBins++;
                continue;
            }

            List<SetPosition> addedList = new List<SetPosition>();
            for (int i = 0; i < binSize; i++)
            {
                current.Count++;
                totalFromCurrent++;
                if (current.Count + current.IndexFrom == allEncodings.Count)
                {
                    addedList.Add((SetPosition)current.Clone());
                    current.Sum++;
                    current.Count = 0;
                    current.IndexFrom = 0;
                    totalFromCurrent = 0;
                    allEncodings = Util.GetEncodings(new List<int>(), symbols, current.Sum, radix);
                }
                else if (i == binSize - 1)
                {
                    addedList.Add((SetPosition)current.Clone());
                    current.IndexFrom = totalFromCurrent;
                    current.Count = 0;
                }
            }
            histogram.Add(centerFrom, addedList);
        }

        return new EncodingHistogram(binSize, histogram, radix, symbols);
    }
}